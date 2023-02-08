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
            service.SetLocation(location_name);
            LocationVM locationVM = new LocationVM()
            {
                Location = service.GetCurrent(),
                Forecast = service.GetForecast()
            };
            return View(locationVM);
        }

    }
}
