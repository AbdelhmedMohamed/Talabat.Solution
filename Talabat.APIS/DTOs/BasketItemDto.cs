using System.ComponentModel.DataAnnotations;

namespace Talabat.APIS.DTOs
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }

        [Required]

        public string ProductName { get; set; }

        [Required]

        public string productUrl { get; set; }

        [Required]
        [Range(0.1, double.MaxValue , ErrorMessage ="Price must greater than zero")]
        public decimal Price { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        [Range(1,int.MaxValue,ErrorMessage = "Quantity must be at least one!")]
        public int Quantity { get; set; }


    }
}