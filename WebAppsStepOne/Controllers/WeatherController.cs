using Microsoft.AspNetCore.Mvc;
using WebAppStepOne.Models;
using WebAppStepOne.Services;
using WebAppStepOne.ViewModels;

namespace WebAppStepOne.Controllers
{
    public class WeatherController : Controller
    {
        WeatherService service = new WeatherService();
        public IActionResult Index()
        {
            return View();
        }

        // POST: Weather/Details/5
        [HttpPost]
        public IActionResult Details(string location_name)
        {
            if (string.IsNullOrWhiteSpace(location_name)) { return Redirect(nameof(Index), "Enter location name!"); }

            try
            {
                service.SetLocation(NormalizeInput(location_name));
                LocationVM locationVM = new LocationVM()
                {
                    Location = service.GetCurrent(),
                    Forecast = service.GetForecast()
                };
                return View(locationVM);

            }
            catch (Exception)
            {
                return Redirect(nameof(Index), "Incorrect location!");
            }
        }

        ActionResult Redirect(string viewName, string message)
        {
            TempData["message"] = message;
            return RedirectToAction(viewName);
        }

        string NormalizeInput(string location_name)
        {
            location_name.ToLower();
            switch (location_name)
            {
                case ("kyiv"):
                case ("київ"):
                case ("киев"):
                    location_name = "kiev";
                    break;
            }
            return location_name;
        }

    }
}
