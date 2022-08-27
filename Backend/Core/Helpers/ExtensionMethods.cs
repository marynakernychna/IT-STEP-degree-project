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
        public static void CheckIdentityResultNullCheck(IdentityResult identityResult)
        {
            if (!identityResult.Succeeded)
            {
                var messageBuilder = new StringBuilder();

                foreach (var error in identityResult.Errors)
                {
                    messageBuilder.AppendLine(error.Description);
                }

                throw new HttpException(
                    messageBuilder.ToString(),
                    HttpStatusCode.BadRequest);
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

        public static void RefreshTokenNullCheck(RefreshToken refreshToken)
        {
            if (refreshToken == null)
            {
                throw new HttpException(
                    ErrorMessages.InvalidToken,
                    HttpStatusCode.NotFound);
            }
        }

        public static void WareNullCheck(Ware ware)
        {
            if (ware == null)
            {
                throw new HttpException(
                    ErrorMessages.WareNotFound,
                    HttpStatusCode.NotFound);
            }
        }

        public static void OrderNullCheck(Order order)
        {
            if (order == null)
            {
                throw new HttpException(
                    ErrorMessages.OrderNotFound,
                    HttpStatusCode.NotFound);
            }
        }
    }
}
