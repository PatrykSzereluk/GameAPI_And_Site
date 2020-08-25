namespace GameWebApi.Features.User.Model
{
    public class ChangePasswordResponseModel
    {
        public bool IsSuccess { get; set; }
        public bool BadPassword { get; set; }
    }
}
