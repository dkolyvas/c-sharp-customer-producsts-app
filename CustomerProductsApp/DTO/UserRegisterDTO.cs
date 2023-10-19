using System.ComponentModel.DataAnnotations;

namespace CustomerProductsApp.DTO
{
    public class UserRegisterDTO
    {
        [Required(ErrorMessage = "email is Required"), EmailAddress(ErrorMessage =
            "You must provide a valid email address")]
        public  string? Email { get; set; }
        [RegularExpression(@"(?=.*[A-Z)(?=.*[a-z])(?=.*\d)(?=.*\W)^.{8,}$", 
            ErrorMessage ="The password must contain at least one capital and one small letter," +
            " one digit and a symbol and be at lest 8 characters long")]
        public string? Password { get; set; }

        public string? ConfirmPassword { get; set; }
    }
}
