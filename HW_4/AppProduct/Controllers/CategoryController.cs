using Microsoft.AspNetCore.Mvc;
using AppProduct.IAbstract;
using AppProduct.Models.DTO;

namespace AppProduct.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IServiceCategory service;

        public CategoryController(IServiceCategory service)
        {
            this.service = service;
        }

        [HttpGet(template: "GetCategory")]
        public IActionResult GetCategory()
        {
            var categorys = service.GetCategories();
            return Ok(categorys);
        }

        [HttpPost(template: ("PostCategory"))]
        public IActionResult AddCategory([FromQuery] string categoryName, string descript)
        {
            try
            {
                var category = new CategoryDto() { Name = categoryName, Descript = descript };
                service.AddCategory(category);
                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete(template: "DelCategody")]
        public IActionResult DeletCategory(string categoryName)
        {
            try
            {
                var category = new CategoryDto() { Name = categoryName };
                service.DeletCategory(category);
                return Ok("Категория продуктов удалена.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
