namespace GameWebApi.Features.Email
{
    public interface IEmailService
    {
        bool SendEmailToUser(string userEmail, string message);
    }
}
