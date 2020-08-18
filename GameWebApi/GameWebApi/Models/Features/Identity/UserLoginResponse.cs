namespace GameWebApi.Models.Features.Identity
{
    public class UserLoginResponse
    {
        public int PlayerId { get; set; }
        public string PlayerNickName { get; set; }
        public string Token { get; set; }
    }
}
