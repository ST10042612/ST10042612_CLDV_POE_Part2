using ABC_Retail.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Azure.Data.Tables;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABC_Retail.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // GET: Home/Index
        public async Task<IActionResult> Index()
        {
            string connectionString = _configuration.GetConnectionString("unicourses");
            var tableClient = new TableClient(connectionString, "Products");
            await tableClient.CreateIfNotExistsAsync();
            List<ProductEntity> customers = new List<ProductEntity>();
            await foreach (var customer in tableClient.QueryAsync<ProductEntity>())
            {
                customers.Add(customer);
            }
            return View(customers);
        }

        public IActionResult Queue()
        {
            return View();
        }
        public IActionResult File()
        {
            return View();
        }
        public IActionResult Blob()
        {
            return View();
        }

        // GET: Home/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        
        // POST: Home/Create
        [HttpPost]
        public async Task<IActionResult> Create(ProductEntity products)
        {
            if (ModelState.IsValid)
            {
                string connectionString = _configuration.GetConnectionString("unicourses");
                var tableClient = new TableClient(connectionString, "Products");
                await tableClient.CreateIfNotExistsAsync();
                await tableClient.AddEntityAsync(products);
                // Set a success message
                TempData["SuccessMessage"] = "Customer information successfully stored!";
            return RedirectToAction(nameof(Index));
            }
            return View(products);
        }
    }
}