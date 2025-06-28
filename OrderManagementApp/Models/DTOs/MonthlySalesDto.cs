namespace OrderManagementApp.Models.DTOs
{
    public class MonthlySalesDto
    {
        public string Month { get; set; }
        public int OrderCount { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}