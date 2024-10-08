using System.ComponentModel.DataAnnotations;

namespace Projekt1.Pages
{
    public class UserInputModel
    {
        [Required(ErrorMessage = "Imię jest wymagane.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email jest wymagany.")]
        [EmailAddress(ErrorMessage = "Niepoprawny format adresu email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Hasło musi mieć co najmniej 6 znaków.")]
        public string Password { get; set; }
    }
}
