using AppStoreg.IAbstract;
using AppStoreg.Models.DTO;

namespace AppStoreg.Mutation
{
    public class MiSimpleMutation
    {
        public bool AddProduct(ProductDto productDto, [Service] IServiceProduct serviceProduct) => serviceProduct.AddProduct(productDto);
        public bool AddCategory(CategoryDto categoryDto, [Service] IServiceCategory serviceCategory) => serviceCategory.AddCategory(categoryDto);
        public bool DeleteProduct(ProductDto productDto, [Service] IServiceProduct serviceProduct) => serviceProduct.DeletProduct(productDto);
        public bool DeleteCategory(CategoryDto categoryDto, [Service] IServiceCategory serviceCategory) => serviceCategory.DeletCategory(categoryDto);
        public bool UpPrise(ProductDto product, [Service] IServiceProduct serviceProduct) => serviceProduct.UpPrise(product);
    }
}
