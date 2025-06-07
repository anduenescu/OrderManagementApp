namespace OrderManagementApp.Models
{
    public class User
    {
        public int? Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; } // stored hashed, not plain
    }
}
