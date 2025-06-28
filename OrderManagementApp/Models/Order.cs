namespace OrderManagementApp.Models
{
    public class Order
    {
        public int Id { get; set; }
        public double  TotalPrice { get; set; }

        public DateTime Date {  get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}
