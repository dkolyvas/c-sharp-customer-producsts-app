using System.ComponentModel.DataAnnotations;

namespace CustomerProductsApp.DTO
{
    public class OrderUpdateDTO
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int ProductId { get; set; }

        [DataType(DataType.Date, ErrorMessage = "You must set a valid date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }
    }
}
