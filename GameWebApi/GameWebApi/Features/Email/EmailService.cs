namespace GameWebApi.Features.Email
{
    using System;
    using System.Net;
    using System.Net.Mail;
    using Microsoft.Extensions.Options;

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

            string def = "<td bgcolor=\"#ddff6f\" style=\"padding: 40px 30px 40px 30px;\"><table border=\"1\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><tr><td>Row 1 </td></tr><tr><td>Row 2 </td></tr><tr><td>Row 3 </td></tr></table></td>";


            try
            {
                var smtpClient = GetSmtpClient();

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_applicationSettings.Email),
                    Subject = "Subject",
                    Body = def,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(_applicationSettings.TestTo);

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
