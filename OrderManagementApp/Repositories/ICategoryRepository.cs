using OrderManagementApp.Models;

namespace OrderManagementApp.Repositories
{
    public interface ICategoryRepository
    {
        public Category? GetCategoryById(int Id);
    }
}
