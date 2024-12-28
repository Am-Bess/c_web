
using Microsoft.AspNetCore.Mvc;
using AppStoregStore.IAbstract;
using AppStoregStore.Models.DTO;
using AppStoregStore.WebClient.IAbstractClient;

namespace AppStoregStore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly IServiceStorage service;
        private readonly IStoregClient storegClient;

        public StorageController(IServiceStorage service, IStoregClient storegClient)
        {
            this.service = service;
            this.storegClient = storegClient;
        }

        [HttpGet(template: "GetPositions")]
        public IActionResult GetPosicions()
        {
            var positions = service.GetPosition();
            return Ok(positions);
        }

        [HttpPost(template: ("PostPosition"))]
        public async Task AddPosition([FromQuery] int productId, string positionName, string descript, int count)
        {
            try
            {
                var productExisTask = storegClient.ExistsProsuct(productId);
                if (await productExisTask)
                {
                    var position = new StorageDto() { productId = productId, Name = positionName, Descript = descript, Count = count };
                    service.AddPosition(position);
                    Ok("Добавлено");
                }
            }
            catch (Exception ex)
            {
                BadRequest(ex.Message);
            }
        }

        [HttpDelete(template: "DelPosition")]
        public IActionResult DeletPosicion(string positionName)
        {
            try
            {
                var position = new StorageDto() { Name = positionName };
                service.DeletPosition(position);
                return Ok("Позиция продуктов удалена.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
