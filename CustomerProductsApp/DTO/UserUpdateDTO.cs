using System.ComponentModel.DataAnnotations;

namespace CustomerProductsApp.DTO
{
    public class UserUpdateDTO
    {
     
        public string? Email { get; set; }
        public string? OldPassword { get; set; }
        [RegularExpression(@"(?=.*[A-Z)(?=.*[a-z])(?=.*\d)(?=.*\W)^.{8,}$",
            ErrorMessage = "The password must contain at least one capital and one small letter," +
            " one digit and a symbol and be at lest 8 characters long")]
        public string? NewPassword { get; set; }

        public string? ConfirmPassword { get; set; }
    }
}
