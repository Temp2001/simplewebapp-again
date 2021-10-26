using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SimpleWebAppMVC.Models;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace SimpleWebAppMVC.Controllers
{
    /**
     * Home Controller
     */
    public class HomeController : Controller
    {
        private readonly ISocialService _socialService;
        private readonly IConfiguration _configuration;

        public HomeController(ISocialService socialService, IConfiguration configuration)
        {
            _socialService = socialService;
            _configuration = configuration;
        }

        // GET /Home/About
        public About About()
        {
            string          location    = Assembly.GetExecutingAssembly().Location;
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(location);

            About model = new About
            {
                AppName   = "MyAppName",
                Copyright = "MIT License",
                Url       = "https://www.jammary.com/",
                Version   = ("Version " + versionInfo.ProductVersion)
            };

            return model;
        }

        // GET /Home/API
        [HttpGet]
        public IActionResult API()
        {
            ViewData["message_short"] = "Tasks API";

            return View();
        }

        // GET /Home/Error
        public IActionResult Error()
        {
            ErrorViewModel model = new ErrorViewModel
            {
                RequestId = (Activity.Current?.Id ?? HttpContext.TraceIdentifier)
            };

            return View(model);
        }

        // GET [ /, /Home/, /Home/Index ]
        public IActionResult Index()
        {
            ViewData["message_short"] = "Welcome to my simple web app";
            ViewData["message_long"]  = "This simple web app is made using ASP.NET Core 3.1 MVC.";

            return View();
        }

        // GET [ /, /Home/, /Home/SocialMedia ]
        public IActionResult SocialMedia(int count)
        {
            ViewData["social-data"] = _socialService.GetSocialData().Take(count).ToList();

            return View();
        }
    }
}
