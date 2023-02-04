using ErrorOr;

namespace BuberDinner.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Authentication
        {
            public static Error InvalidCredentials => Error.Validation("Authentication.InvalidCredentials", "Invalid Email or Password");
            public static Error UnAuthorized => Error.Validation("Authentication.UnAuthorized", "UnAuthorized");
        }
    }
}
