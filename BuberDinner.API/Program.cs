using BuberDinner.API.DependencyInjection;
using BuberDinner.Application.DependencyInjection;
using BuberDinner.Infrastructure.DependencyInjection;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAPIDependencies()
                .AddAPILocalization()
                .AddApplication()
                .AddInfrastructure(builder.Configuration)
                .AddPersistence();

var app = builder.Build();

app.UseExceptionHandler("/error");

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseRouting();

var supportedCultures = new[]
{
                new CultureInfo("ar"),
                new CultureInfo("en"),
};
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

app.UseAuthentication();

app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();
