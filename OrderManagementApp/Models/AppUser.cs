namespace OrderManagementApp.Models
{
    public class AppUser
    {
        public int Id { get; set; }

        public static string UserName { get; set; }

        public static string Password { get; set; }

        public static string Email { get; set; }

        public static List<Product> Cart { get; set; }


    }
}
