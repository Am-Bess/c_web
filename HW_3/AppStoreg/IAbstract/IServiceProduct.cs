using AppStoreg.Models.DTO;

namespace AppStoreg.IAbstract
{
    public interface IServiceProduct
    {
        bool AddProduct(ProductDto product);  
        IEnumerable<ProductDto> GetProducts();
        bool UpPrise(ProductDto product);
        bool DeletProduct(ProductDto product);
        bool CheckProduct(int productId);
        ProductDto GetProduct(int productId); 
    }
}
