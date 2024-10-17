using System.ComponentModel.DataAnnotations;

namespace OrderService.Core.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public required string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}