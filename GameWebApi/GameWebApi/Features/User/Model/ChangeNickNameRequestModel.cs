namespace GameWebApi.Features.User.Model
{
    public class ChangeNickNameRequestModel : BaseRequestData
    {
        public string NewNickName { get; set; }
    }
}
