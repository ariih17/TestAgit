using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using TestAgit.Models;

namespace TestAgit.Controllers
{
    public class PlanController : Controller
    {
        public IActionResult Index(Plan plan)
        {
            var adjustedPlan = plan;
            return View("Index", adjustedPlan);
        }

        [HttpPost]
        public IActionResult AdjustPlan(Plan plan)
        {
            if (ModelState.IsValid)
            {
                int total = plan.Senin + plan.Selasa + plan.Rabu + plan.Kamis + plan.Jumat + plan.Sabtu + plan.Minggu;
                //int avg = total / 7;

                int[] days = { plan.Senin , plan.Selasa , plan.Rabu , plan.Kamis , plan.Jumat , plan.Sabtu , plan.Minggu };
                int[] adjustedDays = new int[7];

                int all = total;

                List<int> sortedDays = days.ToList();
                for (int i = 0; i < sortedDays.Count; i++)
                {
                    if (all > 0 && sortedDays[i] <= 4)
                    {
                        adjustedDays[i] = 4;
                        all -= 4;
                    }
                    else
                    {
                        adjustedDays[i] = 5;
                        all -= 5;
                    }
                }

                plan.Senin = adjustedDays[0];
                plan.Selasa = adjustedDays[1];
                plan.Rabu = adjustedDays[2];
                plan.Kamis = adjustedDays[3];
                plan.Jumat = adjustedDays[4];
                plan.Sabtu = adjustedDays[5];
                plan.Minggu = adjustedDays[6];

                plan.Total = total;

                var adjustedPlan = plan;
                return View("Index", adjustedPlan);
            }

            return View("Index");
        }
    }
}
