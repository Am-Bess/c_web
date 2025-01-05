using AppProduct.Models.DTO;

namespace AppProduct.IAbstract
{
    public interface IServiceCategory
    {
        bool AddCategory(CategoryDto category);
        IEnumerable<CategoryDto> GetCategories();
        bool DeletCategory(CategoryDto product);
    }
}
