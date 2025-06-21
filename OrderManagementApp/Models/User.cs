namespace OrderManagementApp.Models
{
    public class User
    {
        public int? Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; } // stored hashed, not plain
        public string Role { get; set; } = "User"; // default value


    }
}
