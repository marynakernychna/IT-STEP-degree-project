using Core.Entities;
using Core.Exceptions;
using Core.Resources;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Text;

namespace Core.Helpers
{
    public static class ExtensionMethods
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

        public static void UserNullCheck(User user)
        {
            if (user == null)
            {
                throw new HttpException(
                    ErrorMessages.UserNotFound,
                    HttpStatusCode.NotFound);
            }
        }

        public static void CategoryNullCheck(Category category)
        {
            if (category == null)
            {
                throw new HttpException(
                    ErrorMessages.CategoryNotFound,
                    HttpStatusCode.NotFound);
            }
        }
    }
}
