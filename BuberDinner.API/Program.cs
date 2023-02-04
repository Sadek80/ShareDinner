using BuberDinner.API.DependencyInjection;
using BuberDinner.Application.DependencyInjection;
using BuberDinner.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAPIDependencies()
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

app.UseAuthentication();

app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();
