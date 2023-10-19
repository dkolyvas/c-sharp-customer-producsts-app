using System.ComponentModel.DataAnnotations;

namespace CustomerProductsApp.DTO
{
    public class CustomerInsertDTO
    {
        [Required(ErrorMessage = "The field firstname is a required field"),
    MaxLength(50, ErrorMessage = "The field firstname must not exceed 50 characters"),
    RegularExpression(@"^\w*$", ErrorMessage = "The firstname can contain only letters")]
        public string Firstname { get; set; } = null!;

        [Required(ErrorMessage = "The field lastname is a required field"),
      MaxLength(50, ErrorMessage = "The field lastname must not exceed 50 characters"),
      RegularExpression(@"^\w*$", ErrorMessage = "The lastname can contain only letters")]
        public string Lastname { get; set; } = null!;

        [MaxLength(80, ErrorMessage = "Address must not exceed 80 characters")]
        public string? Address { get; set; }

        [MaxLength(15, ErrorMessage = "Phone number cannot exceed 15 digits"),
            RegularExpression(@"^\d*$", ErrorMessage = "Phone number can contain only numbers")]
        public string? Phone { get; set; }
    }
}
