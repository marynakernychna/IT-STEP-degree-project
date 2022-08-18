using Core.DTO.Email;
using Core.Entities;
using Core.Exceptions;
using Core.Helpers;
using Core.Interfaces;
using Core.Interfaces.CustomService;
using Core.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Core.Services
{
    public class EmailService : IEmailService
    {
        private readonly UserManager<User> _userManager;
        private readonly AppSettings _appSettings;
        private readonly ITemplateHelper _templateHelper;

        public EmailService(
            UserManager<User> userManager,
            IOptions<AppSettings> appSettings,
            ITemplateHelper templateHelper)
        {
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _templateHelper = templateHelper;
        }

        public async Task SendConfirmationEmailAsync(User user, string callbackUrl)
        {
            var confirmationToken = await _userManager
                .GenerateEmailConfirmationTokenAsync(user);

            var message = await _templateHelper.GetTemplateHtmlAsStringAsync<ConfirmationEmailDTO>(
            "ConfirmationEmail",
            new ConfirmationEmailDTO
            {
                Name = user.Name,
                Surname = user.Surname,
                Link = callbackUrl + "/" + confirmationToken + "/" + user.Email
            });

            await SendEmailAsync(user.Email, "Confirm your account", message);
        }

        private async Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SendGridClient(_appSettings.SendGridKey);
            var from = new EmailAddress(
                                            _appSettings.SendGridEmail,
                                            _appSettings.SendGridSenderName
                                       );
            var to = new EmailAddress(email, email);
            var plainTextContent = "";
            var msg = MailHelper
                .CreateSingleEmail(from, to, subject, plainTextContent, message);

            var result = await client.SendEmailAsync(msg);

            if (!result.IsSuccessStatusCode)
            {
                throw new HttpException(
                    ErrorMessages.FailedSendEmail,
                    HttpStatusCode.InternalServerError);
            }
        }

        public async Task SendConfirmationResetPasswordEmailAsync(User user, string callbackUrl)
        {
            var confirmationToken = await _userManager
                .GenerateEmailConfirmationTokenAsync(user);

            var message = await _templateHelper.GetTemplateHtmlAsStringAsync<ConfirmationEmailDTO>(
            "ConfirmationResetPasswordEmail",
            new ConfirmationEmailDTO
            {
                Name = user.Name,
                Surname = user.Surname,
                Link = callbackUrl + "/" + confirmationToken + "/" + user.Email
            });

            await SendEmailAsync(user.Email, "Confirm your account", message);
        }
    }
}
