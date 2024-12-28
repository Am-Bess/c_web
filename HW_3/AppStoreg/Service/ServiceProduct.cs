using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using AppStoreg.IAbstract;
using AppStoreg.Models;
using AppStoreg.Models.Context;
using AppStoreg.Models.DTO;

namespace AppStoreg.Service
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

        public bool AddProduct(ProductDto product)
        {
            if (!context.Products.Any(x => x.Name!.Equals(product.Name)))
            {
                var entity = mapper.Map<Product>(product);
                context.Products.Add(entity);
                context.SaveChanges();
                memoryCache.Remove("products");
                memoryCache.Remove("productsCSV");
                return true;
            }
            return false;
        }

        public IEnumerable<ProductDto> GetProducts()
        {
            if (memoryCache.TryGetValue("products", out List<ProductDto> productsCash))
            {
                return productsCash;
            }

            var products = context.Products.Select(x => mapper.Map<ProductDto>(x)).ToList();
            memoryCache.Set("products", products, TimeSpan.FromMinutes(30));
            return products;
        }

        public ProductDto GetProduct(int productId)
        {
            ProductDto product = mapper.Map<ProductDto>(context.Products.Where(x => x.Id == productId).FirstOrDefault());
            return product;
        }

        public bool UpPrise(ProductDto product)
        {
            if (context.Products.Any(x => x.Name!.Equals(product.Name)))
            {
                var entity = context.Products.Where(x => x.Name!.Equals(product.Name)).FirstOrDefault();
                entity!.Price = product.Price;
                context.SaveChanges();
                memoryCache.Remove("products");
                memoryCache.Remove("productsCSV");
                return true;
            }
            return false;
        }

        public bool DeletProduct(ProductDto product)
        {
            if (context.Products.Any(x => x.Name!.Equals(product.Name)))
            {
                var entity = context.Products.Where(x => x.Name!.Equals(product.Name)).FirstOrDefault();
                context.Products.Remove(entity);
                context.SaveChanges();
                memoryCache.Remove("products");
                memoryCache.Remove("productsCSV");
                return true;
            }
            return false;
        }

        public bool CheckProduct(int productId)
        {
            bool resalt = context.Products.Any(x => x.Id == productId);
            return resalt;
        }
    }
}
