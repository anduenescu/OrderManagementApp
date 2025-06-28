namespace OrderManagementApp.Models
{
    public class OrderItem
    {
        public int Id { get; set; } // Optional: if persisted in DB later

        public Product Product { get; set; }
        public Order? Order { get; set; }
        public int Quantity { get; set; }
    }
}
