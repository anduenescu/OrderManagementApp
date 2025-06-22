namespace OrderManagementApp.Models
{
    public class Order
    {
        public int Id { get; set; }
        public double  TotalPrice { get; set; }

        public DateTime Date {  get; set; }
        public string Status { get; set; }
        public AppUser User { get; set; }
        public List<CartItem> Items { get; set; }
    }
}
