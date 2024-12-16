namespace Talabat.APIS.DTOs
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public int ProducrId { get; set; }  //2

        public string ProductName { get; set; }  //Latte
        public string ProductUrl { get; set; } //Ay Soooora

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}