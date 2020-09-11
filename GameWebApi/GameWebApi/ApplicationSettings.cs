namespace GameWebApi
{
    public class ApplicationSettings
    {
        public string Secret { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int PasswordChangePeriod { get; set; }
        public string MailToTest1 { get; set; }
        public string MailToTest2 { get; set; }
        public string MailToTest3 { get; set; }
        public string EmailConfirmationAddress{ get; set; }
    }
}
