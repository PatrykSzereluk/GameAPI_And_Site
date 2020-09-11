using System.Linq;
using GameWebApi.Models.DB;

namespace GameWebApi.Features.Email
{
    using System;
    using System.Net;
    using System.Net.Mail;
    using Microsoft.Extensions.Options;
    using System.IO;
    using System.Threading.Tasks;
    using Models;
    using Utility.Logging;
    using Microsoft.EntityFrameworkCore;

    public class EmailService : IEmailService
    {
        private readonly GameDBContext _context;
        private readonly ApplicationSettings _applicationSettings;
        private readonly SmtpClient _smtpClient;
        public EmailService(IOptions<ApplicationSettings> applicationSettings, GameDBContext context)
        {
            _applicationSettings = applicationSettings.Value;
            _context = context;
            _smtpClient = GetSmtpClient();
        }

        private SmtpClient GetSmtpClient()
        {
            return new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(_applicationSettings.Email,_applicationSettings.Password),
                EnableSsl = true
            };
        }

        public async Task<bool> SendEmailToUser(string userEmail, string message, EmailType emailType, EmailData data = null)
        {
            var emailMessage = await GetMessage(emailType);

            emailMessage.Definition = FillTemplate(emailMessage.Definition, data, emailType);
            try
            {
                if (SendEmail(userEmail, emailMessage.Subject, emailMessage.Definition))
                {
                    Logger.GetInstance().Info($"Send email to: {userEmail} with status 1");
                    return true;
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance().Error($"Send email to: {userEmail} with status 0. {e.InnerException?.Message}");
                return false;
            }

            return false;
        }

        private string FillTemplate(string template, EmailData data, EmailType emailType)
        {
            switch (emailType)
            {
                case EmailType.Welcome:
                case EmailType.ChangePassword:
                {
                    template = template.Replace("r-NickName", data.NickName);
                    template = template.Replace("r-Id", data.NickName);
                    break;
                }

            }

            return template;
        }


        private bool SendEmail(string to,string subject, string message)
        {
            try
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_applicationSettings.Email),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(_applicationSettings.MailToTest1);
                mailMessage.To.Add(to);
                //mailMessage.To.Add(_applicationSettings.MailToTest3);

                _smtpClient.Send(mailMessage);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private async Task<EmailMessage> GetMessage(EmailType emailType)
        {
            var templateName = "";
            var emailMessage = new EmailMessage();
            switch (emailType)
            {
                case EmailType.Welcome:
                {
                    templateName = "WelcomeTemplate";
                    emailMessage.Subject = "Welcome to the game";

                    break;
                }
                case EmailType.ChangePassword:
                {
                    templateName = "ChangePassword";
                    emailMessage.Subject = "Change password";

                    break;
                }
                default:
                    //Logger.GetInstance().Warning($"Not exists emailType: {emailType}");
                    break;
            }

            var definition = await File.ReadAllTextAsync(Path.Join(Directory.GetCurrentDirectory(), "EmailTemplates", templateName + ".html"));
            
            emailMessage.Definition = definition;

            return emailMessage;
        }

        public async Task SendEmailToAll(string message, string subject)
        {
            var users = await _context.PlayerIdentity.ToListAsync();

            Parallel.ForEach(users, (user) => { SendEmail(user.Email, subject, message); });

        }
    }
}
