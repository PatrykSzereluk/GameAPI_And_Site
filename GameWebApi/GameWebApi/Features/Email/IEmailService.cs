namespace GameWebApi.Features.Email
{
    using System.Threading.Tasks;
    using Models;
    public interface IEmailService
    {
         Task<bool> SendEmailToUser(string userEmail, string message, EmailType emailType, EmailData data = null);
    }
}
