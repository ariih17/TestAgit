using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestAgit.Models;

namespace TestAgit.Controllers
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult AdjustPlan(Plan plan)
        {
            if (ModelState.IsValid)
            {
                int total = plan.Senin + plan.Selasa + plan.Rabu + plan.Kamis + plan.Jumat;
                int avg = total / 7;

                plan.Senin = avg;
                plan.Selasa = avg;
                plan.Rabu = avg;
                plan.Kamis = avg;
                plan.Jumat = avg;
                plan.Sabtu = avg;
                plan.Minggu = avg;

                plan.Total = total;

                var adjustedPlan = plan;
                return View("Index", adjustedPlan);
            }

            return View("Index");
        }
    }
}
