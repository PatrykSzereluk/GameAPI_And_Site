namespace GameWebApi.Models.Features.Identity
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterRequestModel
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
