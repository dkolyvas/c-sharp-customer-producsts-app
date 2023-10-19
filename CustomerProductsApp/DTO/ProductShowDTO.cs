using System.ComponentModel.DataAnnotations;

namespace CustomerProductsApp.DTO
{
    public class ProductShowDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        [DataType(DataType.Currency)]
        public decimal? Price { get; set; }

        public int? Quantity { get; set; }
    }
}
