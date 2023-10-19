namespace CustomerProductsApp.DTO
{
    public class CustomerShowDTO
    {
        public int Id { get; set; }

        public string Firstname { get; set; } = null!;

        public string Lastname { get; set; } = null!;

        public string? Address { get; set; }

        public string? Phone { get; set; }
    }
}
