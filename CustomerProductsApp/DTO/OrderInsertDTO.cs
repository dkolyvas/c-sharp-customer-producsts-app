using CustomerProductsApp.Data;
using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace CustomerProductsApp.DTO
{
    public class OrderInsertDTO
    {

        [Required(ErrorMessage ="You must set a customer")]
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "You must set a product")]
        public int ProductId { get; set; }
        [DataType(DataType.Date, ErrorMessage ="You must set a valid date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode =true)]
        public DateTime? Date { get; set; }

       
    }
}
