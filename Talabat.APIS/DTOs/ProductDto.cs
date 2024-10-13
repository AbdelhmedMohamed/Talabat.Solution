using Talabat.Core.Entities;

namespace Talabat.APIS.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string PictureUrl { get; set; }

        public decimal Price { get; set; }

        public int BrandId { get; set; } //FK
        public string Brand { get; set; } //navigation prop [one]

        public int CategoryId { get; set; } //FK
        public string Category { get; set; }

    }
}
