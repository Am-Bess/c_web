using HW_2.Models.DTO;

namespace HW_2.IAbstract
{
    public interface IServiceCategory
    {
        void AddCategory(CategoryDto category);
        IEnumerable<CategoryDto> GetCategories();
        void DeletCategory(CategoryDto product);
    }
}
