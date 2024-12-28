using Microsoft.AspNetCore.Mvc;
using AppStoreg.IAbstract;
using AppStoreg.Models.DTO;

namespace AppStoreg.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IServiceProduct service;

        public ProductController(IServiceProduct service)
        {
            this.service = service;
        }

        [HttpGet(template: "GetProducts")]
        public IActionResult GetProducts()
        {
            var products = service.GetProducts();
            return Ok(products);
        }

        [HttpGet(template: "GetProduct")]
        public IActionResult GetProduct([FromQuery] int productId)
        {
            var products = service.GetProduct(productId);
            return Ok(products);
        }

        [HttpGet(template: "CheckProduct")]
        public IActionResult CheckProduct([FromQuery] int productId)
        {
            var products = service.CheckProduct(productId);
            return Ok(products);
        }

        [HttpPost(template: "PostProduct")]
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

        [HttpPut("UpdatePrice")]
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

        [HttpDelete(template: "DelProduct")]
        public IActionResult DeletProduct(string nameProduct)
        {
            try
            {
                var product = new ProductDto() { Name = nameProduct };
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
