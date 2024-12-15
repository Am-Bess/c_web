using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using HW_2.IAbstract;
using HW_2.Models;
using HW_2.Models.Context;
using HW_2.Models.DTO;

namespace HW_2.Service
{
    public class ServiceProduct : IServiceProduct
    {
        private readonly IMapper mapper;
        private IMemoryCache memoryCache;
        private readonly ProductContext context;

        public ServiceProduct(IMapper mapper, IMemoryCache memoryCache, ProductContext context)
        {
            this.mapper = mapper;
            this.memoryCache = memoryCache;
            this.context = context;
        }

        public void AddProduct(ProductDto product)
        {
            if (!context.Products.Any(x => x.Name.Equals(product.Name)))
            {
                var entity = mapper.Map<Product>(product);
                context.Products.Add(entity);
                context.SaveChanges();
                memoryCache.Remove("products");
                memoryCache.Remove("productsCSV");
            }
        }

        public IEnumerable<ProductDto> GetProducts()
        {
            if (memoryCache.TryGetValue("products", out List<ProductDto>? productsCash))
            {
                return productsCash;
            }

            var products = context.Products.Select(x => mapper.Map<ProductDto>(x)).ToList();
            memoryCache.Set("products", products, TimeSpan.FromMinutes(30));
            return products;
        }

        public void UpPrise(ProductDto product)
        {
            if (context.Products.Any(x => x.Name.Equals(product.Name)))
            {
                var entity = context.Products.Where(x => x.Name.Equals(product.Name)).FirstOrDefault();
                entity!.Price = product.Price;
                context.SaveChanges();
                memoryCache.Remove("products");
                memoryCache.Remove("productsCSV");
            }
        }

        public void DeletProduct(ProductDto product)
        {
            if (context.Products.Any(x => x.Name.Equals(product.Name)))
            {
                var entity = context.Products.Where(x => x.Name.Equals(product.Name)).FirstOrDefault();
                context.Products.Remove(entity);
                context.SaveChanges();
                memoryCache.Remove("products");
                memoryCache.Remove("productsCSV");
            }
        }

    }
}
