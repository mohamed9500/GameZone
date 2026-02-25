using System.Diagnostics;
using GameZone.Models;
using GameZone.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameZone.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGamesServices _gamesservices;

        public HomeController(IGamesServices gamesservices)
        {
            _gamesservices = gamesservices;
        }

        public IActionResult Index()
        {
            var games = _gamesservices.GetAll();
            Console.WriteLine($"Games Count: {games.Count()}"); // اختبار
            return View(games);
        }

      


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
