using Microsoft.AspNetCore.Mvc;
using HW_1.Models;
using HW_1.Models.ModelAnswer;

namespace HW_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductContext? _context;

        public ProductController(ProductContext context)
        {
            _context = context;
        }

        [HttpGet(template: "GetProduct")] // Получение продукта
        public IActionResult GetProduct()
        {
            try
            {
                var product = _context?.Products?.Select(x => new GetProduct()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Descript = x.Descript,
                    Price = x.Price,
                    CategoriId = x.CategoriId,

                }).ToList();
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet(template: "GetCategory")] // Получение категорий
        public IActionResult GetCategory()
        {
            try
            {
                var category = _context?.Categories?.Select(x => new GetCategory()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Descript = x.Descript,
                }).ToList();
                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost(template: ("PostCategory"))] // Добавление новой категории.
        public IActionResult AddCategory([FromQuery] string categoryName, string descript)
        {
            try
            {
                if (!_context!.Categories!.Any(x => x.Name!.Equals(categoryName)))
                {
                    _context.Add(new Category()
                    {
                        Name = categoryName,
                        Descript = descript
                    });
                    _context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return StatusCode(409, "Категория с этим наименованием уже существует");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost(template: "PostProduct")] // Добавление продукта
        public IActionResult Post([FromQuery] string name, string descript, int categorId, int price)
        {
            try
            {
                if (_context!.Categories!.Any(x => x.Id == categorId))
                {
                    if (!_context!.Products!.Any(x => x.Name!.Equals(name)))
                    {
                        _context.Add(new Product()
                        {
                            Name = name,
                            Descript = descript,
                            Price = price,
                            CategoriId = categorId
                        });
                        _context.SaveChanges();
                        return Ok();
                    }
                    else
                    {
                        return StatusCode(409, "Товар с таким наименованием уже существует.");
                    }
                }
                else
                {
                    return NotFound("Указанной категории товаров не существует.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("UpdatePrice")] // Добавление или изменение цены.
        public IActionResult PutPrise([FromQuery] string nameProduct, int price)
        {
            try
            {
                if (_context!.Products!.Any(x => x.Name!.Equals(nameProduct)))
                {
                    var product = _context?.Products?.Where(x => x.Name == nameProduct).FirstOrDefault();
                    product!.Price = price;
                    _context?.SaveChanges();
                    return Ok("Цена обновлена!");
                }
                else
                {
                    return NotFound("Указанный продукт не найден");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete(template: "DelProduct")] // Удаляет продукт
        public IActionResult DeletProduct(string nameProduct)
        {
            try
            {
                if (_context!.Products!.Any(x => x.Name!.Equals(nameProduct)))
                {
                    var product = _context.Products?.Where(x => x.Name == nameProduct).FirstOrDefault();
                    _context.Products?.Remove(product!);
                    _context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return NotFound("Указанный товар не найден.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete(template: "DelCategody")] // Удаляет категорию
        public IActionResult DeletCategory(string category)
        {
            try
            {
                if (_context!.Categories!.Any(x => x.Name!.Equals(category)))
                {
                    var group = _context.Categories?.Where(x => x.Name == category).FirstOrDefault();
                    var groupProduct = _context?.Products?.Where(x => x.CategoriId == group!.Id).ToList();
                    if (groupProduct!.Any()) _context?.Products?.RemoveRange(groupProduct!);
                    _context?.Categories?.Remove(group!);
                    _context?.SaveChanges();
                    return Ok();
                }
                else
                {
                    return NotFound("Указанная категория не найдена");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
