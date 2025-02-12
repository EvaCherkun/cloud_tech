using Microsoft.AspNetCore.Mvc;
using Microsoft.ApplicationInsights;
using System.Diagnostics;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TelemetryClient _telemetryClient;

        public HomeController(ILogger<HomeController> logger, TelemetryClient telemetryClient)
        {
            _logger = logger;
            _telemetryClient = telemetryClient;
        }

        public IActionResult Index()
        {
            _telemetryClient.TrackEvent("Index Page Loaded");
            _telemetryClient.TrackTrace("User visited the Index page.");
            _telemetryClient.TrackMetric("IndexPageVisits", 1);
            return View();
        }


        public IActionResult TestError()
        {
            throw new Exception("Test exception for telemetry");
        }


        public IActionResult Privacy()
        {
            _telemetryClient.TrackEvent("Privacy Page Opened");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _telemetryClient.TrackException(new Exception("An error occurred"));
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
