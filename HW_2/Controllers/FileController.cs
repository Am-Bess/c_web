using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HW_2.IAbstract;

namespace HW_2.Controllers
{
    [Route("File/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IServiceFiles service;

        public FileController(IServiceFiles service)
        {
            this.service = service;
        }

        [HttpGet(template: "GetFileCSV")]
        public ActionResult<string> GetFileCSV()
        {
            try
            {
                string? path = $"https://{Request.Host.ToString()}/static/{service.GetProductCsvUrl()}";
                return path;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet(template: "DownloadFileCSV")]
        public FileContentResult? DownloadFileCSV()
        {
            try
            {
                return File(new System.Text.UTF8Encoding().GetBytes(service.GetProductCsv()), "text/csv", "report.csv");
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpGet(template: "GetStatictic")]
        public ActionResult<string> GetStatictic()
        {
            try
            {
                string path = $"https://{Request.Host.ToString()}/static/{service.GetStatistic()}";
                return path;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
