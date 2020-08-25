namespace GameWebApi.Features.Identity.Models
{
    public class UserRegisterResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public int PlayerId { get; set; }
        public string NickName { get; set; }
    }
}
