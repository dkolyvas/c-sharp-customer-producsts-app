using CustomerProductsApp.Data;

namespace CustomerProductsApp.DTO
{
    public class OrderShowDTO
    {
        public int Id { get; set; }

        public int? CustomerId { get; set; }

        public int? ProductId { get; set; }

        public DateTime? Date { get; set; }

        public string? CustomerName { get; set; }

        public string? ProductName { get; set; }
    }
}
