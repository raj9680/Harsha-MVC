using FromHeader.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace FromHeader.Models
{
    public class OrderModel
    {
        public int? OrderNo { get; set; }

        [MinimumDateValidator("2000-01-01", ErrorMessage = "Order Date should be greater than or equal to 2000")] //custom validator
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "{0} can't be blank")]
        [Display(Name = "Invoice Price")]
        [InvoicePriceValidator] //custom validator
        public double InvoicePrice { get; set; }

        [ProductsListValidator] //custom validator
        public List<Product> Products { get; set; }  = new List<Product>();
    }

    public class Product
    {
        [Required(ErrorMessage = "{0} can't be blank")]
        [Display(Name = "Product Code")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} should be between a valid number")]
        public int ProductCode { get; set; }

        [Required(ErrorMessage = "{0} can't be blank")]
        [Display(Name = "Product Price")]
        [Range(1, double.MaxValue, ErrorMessage = "{0} should be between a valid number")]
        public double Price { get; set; }

        [Required(ErrorMessage = "{0} can't be blank")]
        [Display(Name = "Product Quantity")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} should be between a valid number")]
        public int Quantity { get; set; }
    }
}
