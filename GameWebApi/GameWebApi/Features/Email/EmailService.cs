using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GameWebApi.Features.Email.Models;
using GameWebApi.Features.Utility.Log;

namespace GameWebApi.Features.Email
{
    using System;
    using System.Net;
    using System.Net.Mail;
    using Microsoft.Extensions.Options;

    public class EmailService : IEmailService
    {
        private readonly ApplicationSettings _applicationSettings;
        private readonly SmtpClient _smtpClient;
        public EmailService(IOptions<ApplicationSettings> applicationSettings)
        {
            _applicationSettings = applicationSettings.Value;
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
            var template = await GetTemplate(emailType);

            message = FillTemplate(template, data, emailType);

            if (SendEmail(userEmail, message))
            {
                Logger.GetInstance().Info($"Send email to: {userEmail} with status 1");
                return true;
            }
            Logger.GetInstance().Info($"Send email to: {userEmail} with status 0");
            return false;
        }

        private string FillTemplate(string template, EmailData data, EmailType emailType)
        {
            switch (emailType)
            {
                case EmailType.Welcome:
                {
                    template = template.Replace("r-NickName", data.NickName);
                    template = template.Replace("r-Id", data.NickName);
                    break;
                }
            }

            return template;
        }


        private bool SendEmail(string to, string message)
        {
            try
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_applicationSettings.Email),
                    Subject = "Subject",
                    Body = message,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(_applicationSettings.TestTo);

                _smtpClient.Send(mailMessage);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }



        private async Task<string> GetTemplate(EmailType emailType)
        {
            var templateName = "";
            switch (emailType)
            {
                case EmailType.Welcome:
                {
                    templateName = "WelcomeTemplate";
                    break;
                }

                default:
                    Logger.GetInstance().Warning($"Not exists emailType: {emailType}");
                    break;
            }

            var definition = await File.ReadAllTextAsync(Path.Join(Directory.GetCurrentDirectory(), "EmailTemplates", templateName + ".html"));

            return definition;
        }

    }
}
