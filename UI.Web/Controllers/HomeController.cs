using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UI.Web.Models;

namespace UI.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            using var httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("", "");

            var responseMessage = await httpClient.GetAsync("http://localhost:52703/api/categories");

            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            JsonConvert.DeserializeObject<List<Category>>(jsonString);
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            //StringContent content = new StringContent()
            var httpClient = new HttpClient();
            var jsonCategory = JsonConvert.SerializeObject(category);
            StringContent content = new StringContent(jsonCategory, Encoding.UTF8, "application/json");
            var responseMessage = await httpClient.PostAsync("http://localhost:52703/api/categories", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Bir sorun oluştu.");
            return View(category);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var httpClient = new HttpClient();
            var responseMessage = await httpClient.GetAsync("http://localhost:52703/api/categories/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonCategory = await responseMessage.Content.ReadAsStringAsync();
                var category = JsonConvert.DeserializeObject<Category>(jsonCategory);

                return View(category);
            }
            ModelState.AddModelError("", "Bir sorun oluştu.");
            return RedirectToAction("Index");
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
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
