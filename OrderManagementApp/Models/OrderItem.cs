namespace OrderManagementApp.Models
{
    public class OrderItem
    {
        public Product Product { get; set; }
        public Order Order { get; set; }
        public int Quantity { get; set; }
    }
}
