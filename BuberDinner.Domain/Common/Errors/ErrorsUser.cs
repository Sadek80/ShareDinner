using ErrorOr;

namespace BuberDinner.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class User
        {
            public const string DuplicateEmailCode = "User.DuplicateEmail.Code";
            public const string DuplicateEmailDescription = "User.DuplicateEmail.Description";
        }
    }
}
