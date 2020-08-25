namespace GameWebApi.Features.User.Model
{
    public class ChangePasswordRequestModel : BaseRequestData
    {
        public string Password { get; set; }
        public string NewPassword { get; set; }
    }
}
