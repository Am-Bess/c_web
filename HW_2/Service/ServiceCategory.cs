﻿using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using HW_2.IAbstract;
using HW_2.Models;
using HW_2.Models.Context;
using HW_2.Models.DTO;

namespace HW_2.Service
{
    public class ServiceCategory : IServiceCategory
    {
        private readonly IMapper mapper;
        private IMemoryCache memoryCache;
        private readonly ProductContext context;

        public ServiceCategory(IMapper mapper, IMemoryCache memoryCache, ProductContext context)
        {
            this.mapper = mapper;
            this.memoryCache = memoryCache;
            this.context = context;
        }

        public void AddCategory(CategoryDto category)    // Добавление категории.
        {
            if (!context.Categories.Any(x => x.Name.Equals(category.Name)))
            {
                var entity = mapper.Map<Category>(category);
                context.Categories.Add(entity);
                context.SaveChanges();
                memoryCache.Remove("categorys");
            }
        }

        public IEnumerable<CategoryDto> GetCategories() // Получение категории.
        {
            if (memoryCache.TryGetValue("categorys", out List<CategoryDto> categoriesCash))
            {
                return categoriesCash;
            }

            var categorys = context.Categories.Select(x => mapper.Map<CategoryDto>(x)).ToList();
            memoryCache.Set("categorys", categorys, TimeSpan.FromMinutes(30));
            return categorys;
        }

        public void DeletCategory(CategoryDto category)
        {
            if (context.Categories.Any(x => x.Name.Equals(category.Name))) // Проверяем, есть ли такая категория.
            {
                var entity = context.Categories.Where(x => x.Name.Equals(category.Name)).FirstOrDefault();
                var groupProduct = context.Products.Where(x => x.Id.Equals(category.Id)).ToList();
                if (groupProduct.Any()) context.Products.RemoveRange(groupProduct); // Удаляем товары, предварительно проверив, что в категории хоть что=то есть.
                context.Categories.Remove(entity); // Удаялем Группу.
                context.SaveChanges(); // Сохраняем изменения.
                memoryCache.Remove("categorys");    // Чистим кэши.
                memoryCache.Remove("products");
                memoryCache.Remove("productsCSV");
            }
        }

    }
}