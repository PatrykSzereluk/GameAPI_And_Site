using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace GameWebApi.Features.Email
{
    public class EmailService : IEmailService
    {
        private readonly ApplicationSettings _applicationSettings;
        public EmailService(IOptions<ApplicationSettings> applicationSettings)
        {
            _applicationSettings = applicationSettings.Value;
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

        public bool SendEmailToUser(string userEmail, string message)
        {
            try
            {
                var smtpClient = GetSmtpClient();

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_applicationSettings.Email),
                    Subject = "Subject",
                    Body = "<h1>Hej<h1>",
                    IsBodyHtml = true
                };
                mailMessage.To.Add("test");

                smtpClient.Send(mailMessage);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
