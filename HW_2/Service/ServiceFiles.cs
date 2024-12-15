using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using System.Text;
using HW_2.IAbstract;
using HW_2.Models.Context;
using HW_2.Models.DTO;

namespace HW_2.Service
{
    public class ServiceFiles : IServiceFiles
    {
        private readonly IMapper mapper;
        private IMemoryCache memoryCache;
        private readonly ProductContext context;

        public ServiceFiles(IMapper mapper, IMemoryCache memoryCache, ProductContext context)
        {
            this.mapper = mapper;
            this.memoryCache = memoryCache;
            this.context = context;
        }

        public string GetProductCsvUrl()
        {
            string? content;
            if (memoryCache.TryGetValue("productsCSV", out List<ProductDto>? productsCash))
            {
                content = GetCsv(productsCash);
            }
            else
            {
                var products = context.Products.Select(x => mapper.Map<ProductDto>(x)).ToList();
                content = GetCsv(products);
                memoryCache.Set("productsCSV", products, TimeSpan.FromMinutes(30));
            }

            string fileName = "Product_list.csv";
            string statisticFileName = "CachStat.txt";
            string directoryName = "StaticFiles";
            File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), directoryName, fileName), content);
            return fileName;
        }

        public string GetProductCsv()
        {
            var content = "";
            if (memoryCache.TryGetValue("productsCSV", out List<ProductDto>? productsCash))
            {
                content = GetCsv(productsCash);
            }
            else
            {
                var products = context.Products.Select(x => mapper.Map<ProductDto>(x)).ToList();
                content = GetCsv(products);
                memoryCache.Set("productsCSV", products, TimeSpan.FromMinutes(30));
            }
            return content;
        }

        public string GetStatistic()
        {
            string statistic = GetCach(memoryCache.GetCurrentStatistics());
            string statisticFileName = "CachStat.csv";
            string directoryName = "StaticFiles";
            File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), directoryName, statisticFileName), statistic);
            return statisticFileName;
        }

        private string GetCsv(IEnumerable<ProductDto> products)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in products)
            {
                sb.AppendLine($"Id: {item.Id}; Name: {item.Name}; Descript: {item.Descript}; Price: {item.Price}; CategoriId: {item.CategoriId};");
            }
            return sb.ToString();
        }

        private string GetCach(MemoryCacheStatistics statistics)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Entry Count: {statistics.CurrentEntryCount.ToString()}, EstimatedSize: {statistics.CurrentEstimatedSize.ToString()}, TotalHits: {statistics.TotalHits.ToString()}, TotalMisses: {statistics.TotalMisses.ToString()}");
            return sb.ToString();
        }
    }
}
