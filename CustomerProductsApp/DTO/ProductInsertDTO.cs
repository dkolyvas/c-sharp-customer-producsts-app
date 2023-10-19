using System.ComponentModel.DataAnnotations;

namespace CustomerProductsApp.DTO
{
    public class ProductInsertDTO
    {

        [Required(ErrorMessage ="Product name is required"), 
            MaxLength(50, ErrorMessage ="Product name must not exceed 50 characters")]
        public string Name { get; set; } = null!;

        [MaxLength(255,ErrorMessage = "Product description must not exceed 255 characters")]
        public string? Description { get; set; }
        [DataType(DataType.Currency, ErrorMessage ="You must set a valid money amount")]
        public decimal? Price { get; set; }

        public int? Quantity { get; set; }
    }
}
