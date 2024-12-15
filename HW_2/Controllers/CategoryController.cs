using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HW_2.IAbstract;
using HW_2.Models.DTO;

namespace HW_2.Controllers
{
    [Route("Category/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IServiceCategory service;  // Интерфейс для работы с категориями

        public CategoryController(IServiceCategory service)
        {
            this.service = service;
        }

        [HttpGet(template: "GetCategory")] // Получение категорий
        public IActionResult GetCategory()
        {
            var categorys = service.GetCategories();
            return Ok(categorys);
        }

        [HttpPost(template: ("PostCategory"))] // Добавление новой категории.
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

        [HttpDelete(template: "DelCategody")]  //Удаление категории и всех товаров относящихся к ней.
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
