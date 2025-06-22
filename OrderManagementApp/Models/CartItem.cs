namespace OrderManagementApp.Models
{
    public class CartItem
    {
        public int Id { get; set; } // Optional: if persisted in DB later

    
        public Product Product { get; set; } 
        public int Quantity { get; set; }    // How many of this product in the cart

     
        public Order? Order { get; set; }    // Back-reference to the order this item belongs to
    }
}

