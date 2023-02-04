using BuberDinner.API.Common.Http;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.API.Controllers
{
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        protected IActionResult Problem(List<Error> errors)
        {
            HttpContext.Items[HttpContextItemsKeys.Errors] = errors;

            var firstError = errors[0];

            var statusCode = firstError.Type switch
            {
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };

            return Problem(title: firstError.Description, statusCode: statusCode);
        }
    }
}
