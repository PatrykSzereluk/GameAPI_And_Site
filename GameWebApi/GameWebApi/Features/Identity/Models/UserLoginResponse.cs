namespace GameWebApi.Features.Identity.Models
{
    public class UserLoginResponse
    {
        public int PlayerId { get; set; }
        public string PlayerNickName { get; set; }
        public string Token { get; set; }
        public string GameToken { get; set; }
        public bool AskAboutChangePassword { get; set; }
        public bool IsBanned { get; set; }
        public bool EmailIsNotConfirmed { get; set; }
    }
}
