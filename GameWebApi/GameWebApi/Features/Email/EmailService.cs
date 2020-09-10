using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GameWebApi.Features.Email.Models;


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
            var emailMessage = await GetMessage(emailType);

            emailMessage.Definition = FillTemplate(emailMessage.Definition, data, emailType);

            if (SendEmail(userEmail,emailMessage.Subject, emailMessage.Definition))
            {
             //   Logger.GetInstance().Info($"Send email to: {userEmail} with status 1");
                return true;
            }
            //Logger.GetInstance().Info($"Send email to: {userEmail} with status 0");
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
                //mailMessage.To.Add(_applicationSettings.MailToTest2);
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

                default:
                    //Logger.GetInstance().Warning($"Not exists emailType: {emailType}");
                    break;
            }

            var definition = await File.ReadAllTextAsync(Path.Join(Directory.GetCurrentDirectory(), "EmailTemplates", templateName + ".html"));
            
            emailMessage.Definition = definition;

            return emailMessage;
        }

    }
}
