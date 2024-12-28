﻿using AppStoreg.IAbstract;
using AppStoreg.Models.DTO;

namespace AppStoreg.Querty
{
    public class MySimpleQuery
    {
        public IEnumerable<ProductDto> GetProducs([Service] IServiceProduct serviceProduct) => serviceProduct.GetProducts();
        public ProductDto GetProduc(int productId, [Service] IServiceProduct serviceProduct) => serviceProduct.GetProduct(productId);
        public bool CheckProducs(int productId, [Service] IServiceProduct serviceProduct) => serviceProduct.CheckProduct(productId);
        public IEnumerable<CategoryDto> GetCatlog([Service] IServiceCategory serviceCategory) => serviceCategory.GetCategories();
    }
}