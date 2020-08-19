namespace GameWebApi.Models.Features.Identity.Models
{
    public class UserLoginResponse
    {
        public int PlayerId { get; set; }
        public string PlayerNickName { get; set; }
        public string Token { get; set; }
        public bool AskAboutChangePassword { get; set; }
    }
}
