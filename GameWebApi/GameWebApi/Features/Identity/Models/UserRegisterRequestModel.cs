namespace GameWebApi.Models.Features.Identity.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UserRegisterRequestModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
