using ErrorOr;

namespace BuberDinner.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Authentication
        {
            public const string InvalidCredentialsCode = "Authentication.InvalidCredentials.Code";
            public const string InvalidCredentialsDescription = "Authentication.InvalidCredentials.Description";

            public const string UnAuthorizedCode = "Authentication.UnAuthorized.Code";
            public const string UnAuthorizedDescription = "Authentication.UnAuthorized.Description";
        }
    }
}
