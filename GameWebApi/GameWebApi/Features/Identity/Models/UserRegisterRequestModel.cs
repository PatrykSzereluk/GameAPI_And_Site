namespace GameWebApi.Features.Identity.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UserRegisterRequestModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string NickName { get; set; }
        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
