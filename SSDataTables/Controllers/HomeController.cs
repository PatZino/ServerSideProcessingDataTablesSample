using Microsoft.AspNetCore.Mvc;
using SSDataTables.Models;
using System.Diagnostics;

namespace SSDataTables.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetData(int draw, int start, int length, string searchValue, List<DataTableOrder> order)
        {
            try
            {
                // Simulating data retrieval from a database or any other source
                var data = Enumerable.Range(1, 100).Select(i => new
                {
                    Id = i,
                    Name = $"Name {i}",
                    // Other properties
                }).ToList();
                //var data = YourDataRepository.GetData();

                // Apply search filtering
                if (!string.IsNullOrWhiteSpace(searchValue))
                {
                    searchValue = searchValue.ToLower(); // Convert to lowercase for case-insensitive search

                    // Filter data based on the search value in the 'Name' field (adjust as needed)
                    data = data.Where(d =>
                        d.Name.ToLower().Contains(searchValue)
                    // Add more conditions for other fields if necessary
                    ).ToList();
                }

                // Apply sorting
                if (order != null && order.Any())
                {
                    var sortColumn = order[0].Column; // Index of the sorting column
                    var sortDirection = order[0].Dir; // Sorting direction ('asc' or 'desc')

                    switch (sortColumn)
                    {
                        case 0: // Sort by the first column (adjust as needed)
                            data = sortDirection == "asc" ? data.OrderBy(d => d.Id).ToList() : data.OrderByDescending(d => d.Id).ToList();
                            break;
                        case 1: // Sort by the second column (adjust as needed)
                            data = sortDirection == "asc" ? data.OrderBy(d => d.Name).ToList() : data.OrderByDescending(d => d.Name).ToList();
                            break;
                            // Add more cases for other columns
                    }
                }

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
            catch (Exception ex)
            {
                // Handle exception appropriately (logging, error response, etc.)
                return StatusCode(500, "An error occurred while fetching data.");
            }

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}