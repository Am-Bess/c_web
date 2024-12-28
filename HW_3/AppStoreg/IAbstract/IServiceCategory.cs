using AppStoreg.Models.DTO;

namespace AppStoreg.IAbstract
{
    public interface IServiceCategory
    {
        bool AddCategory(CategoryDto category);
        IEnumerable<CategoryDto> GetCategories();
        bool DeletCategory(CategoryDto product);
    }
}
