using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using HW_2.IAbstract;
using HW_2.Models.DTO;

namespace HW_2.Controllers
{

    [Route("Product/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IServiceProduct service;

        public ProductController(IServiceProduct service)
        {
            this.service = service;
        }

        [HttpGet(template: "GetProduct")] // Получение продукта
        public IActionResult GetProduct() 
        {
            var products = service.GetProducts();
            return Ok(products);
        }

        [HttpPost(template: "PostProduct")] // Добавление продукта
        public IActionResult Post([FromQuery] string name, string descript, int categorId, int price)
        {
            try
            {
                var product = new ProductDto() { Name = name, Descript = descript, Price = price, CategoriId = categorId };
                service.AddProduct(product);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdatePrice")] // Добавление или изменение цены.
        public IActionResult UpPrise([FromQuery] string nameProduct, int price)
        {
            try
            { 
                var product = new ProductDto() { Name = nameProduct, Price = price };
                service.UpPrise(product);
                return Ok("Цена обновлена!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete(template:"DelProduct")] // Удаление продукта.
        public IActionResult DeletProduct(string nameProduct)
        {
            try
            {
                var product = new ProductDto() { Name = nameProduct};
                service.DeletProduct(product);
                return Ok("Продукт удалён.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
