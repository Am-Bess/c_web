using Microsoft.AspNetCore.Mvc;
using HW_2.Models.DTO;

namespace HW_2.IAbstract
{
    public interface IServiceProduct
    {
        void AddProduct(ProductDto product);
        IEnumerable<ProductDto> GetProducts();
        void UpPrise(ProductDto product);
        void DeletProduct(ProductDto product);
    }
}
