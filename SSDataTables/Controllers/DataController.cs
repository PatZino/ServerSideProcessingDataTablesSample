using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SSDataTables.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetData(int draw, int start, int length)
        {
            // Simulating data retrieval from a database or any other source
            var data = Enumerable.Range(1, 100).Select(i => new
            {
                Id = i,
                Name = $"Name {i}",
                // Other properties
            }).ToList();
            //var data = YourDataRepository.GetData();

            // Paging
            var dataPage = data.Skip(start).Take(length).ToList();

            return Ok(new
            {
                draw,
                recordsTotal = data.Count,
                recordsFiltered = data.Count,
                data = dataPage
            });
        }
    }
}
