using Core.DTO.Email;
using Core.Entities;
using Core.Exceptions;
using Core.Helpers;
using Core.Interfaces;
using Core.Interfaces.CustomService;
using Core.Resources;
using Core.Specifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;
using System.Threading.Tasks;

namespace Core.Services
{
    public class EmailService : IEmailService
    {
        private readonly AppSettings _appSettings;

        private readonly UserManager<User> _userManager;

        private readonly ITemplateHelper _templateHelper;

        private readonly IRepository<User> _userRepository;

        public EmailService(
            IOptions<AppSettings> appSettings,
            UserManager<User> userManager,
            ITemplateHelper templateHelper,
            IRepository<User> userRepository)
        {
            _appSettings = appSettings.Value;
            _userManager = userManager;
            _templateHelper = templateHelper;
            _userRepository = userRepository;
        }

        public async Task SendConfirmationEmailAsync(
            User user,
            string callbackUrl)
        {
            var confirmationToken = await _userManager
                .GenerateEmailConfirmationTokenAsync(user);

            var message = await _templateHelper
                .GetTemplateHtmlAsStringAsync<ConfirmationEmailDTO>(
                    "ConfirmationEmail",
                    new ConfirmationEmailDTO
                    {
                        Name = user.Name,
                        Surname = user.Surname,
                        Link = callbackUrl + "confirm-email/" +
                            confirmationToken + "/" + user.Email
                    });

            await SendEmailAsync(user.Email, "Confirm your account", message);
        }

        public async Task SendResetPasswordRequestAsync(
            User user,
            string callbackUrl)
        {
            var resetPasswordToken = await _userManager
                .GeneratePasswordResetTokenAsync(user);

            var message = await _templateHelper
                .GetTemplateHtmlAsStringAsync<ConfirmationEmailDTO>(
                    "ConfirmationResetPasswordEmail",
                    new ConfirmationEmailDTO
                    {
                        Name = user.Name,
                        Surname = user.Surname,
                        Link = callbackUrl + "reset-password/" +
                            resetPasswordToken + "/" + user.Email
                    });

            await SendEmailAsync(user.Email, "Confirm password reset", message);
        }

        private async Task SendEmailAsync(
            string email,
            string subject,
            string message)
        {
            var client = new SendGridClient(_appSettings.SendGridKey);
            var from = new EmailAddress(
                _appSettings.SendGridEmail,
                _appSettings.SendGridSenderName);

            var to = new EmailAddress(email, email);
            var plainTextContent = "";
            var msg = MailHelper
                .CreateSingleEmail(from, to, subject, plainTextContent, message);

            var result = await client.SendEmailAsync(msg);

            if (!result.IsSuccessStatusCode)
            {
                throw new HttpException(
                    ErrorMessages.THE_MAIL_SENDING_ERROR,
                    HttpStatusCode.InternalServerError);
            }
        }

        public async Task ConfirmEmailAsync(
            EmailConfirmationTokenRequestDTO emailConfirmationTokenRequestDTO)
        {
            var user = await _userRepository.SingleOrDefaultAsync(
                new UserSpecification.GetByEmail(emailConfirmationTokenRequestDTO.Email));

            ExtensionMethods.UserNullCheck(user);

            if (!user.EmailConfirmed)
            {
                var confirm = await _userManager
                    .ConfirmEmailAsync(user, emailConfirmationTokenRequestDTO.Token);

                if (!confirm.Succeeded)
                {
                    throw new HttpException(
                        ErrorMessages.THE_MAIL_SENDING_ERROR,
                        HttpStatusCode.BadRequest);
                }

                user.EmailConfirmed = true;

                await _userManager.UpdateAsync(user);
            }
            else
            {
                throw new HttpException(
                    ErrorMessages.THE_MAIL_SENDING_ERROR,
                    HttpStatusCode.BadRequest);
            }
        }
    }
}
