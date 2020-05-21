using Microsoft.AspNetCore.Mvc;

namespace DutchTreat.Controllers.Drums
{
    public class DrumsReportsController: Controller
    {
        public IActionResult CreateReportCard()
        {
            ViewBag.Title = "Create Report Card Controller";
            return View();
        }
    }
}