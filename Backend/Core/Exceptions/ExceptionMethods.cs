using Core.Resources;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Text;

namespace Core.Exceptions
{
    public static class ExceptionMethods
    {
        public static void CheckIdentityResult(IdentityResult identityResult)
        {
            if (!identityResult.Succeeded)
            {
                var messageBuilder = new StringBuilder();

                foreach (var error in identityResult.Errors)
                {
                    messageBuilder.AppendLine(error.Description);
                }

                throw new HttpException(messageBuilder.ToString(), HttpStatusCode.BadRequest);
            }
        }

        public static void IdentityRoleNullCheck(IdentityRole role)
        {
            if (role == null)
            {
                throw new HttpException(
                    ErrorMessages.IdentityRoleNotFound,
                    HttpStatusCode.NotFound);
            }
        }
    }
}
