using BrouwerWebApp.Models;
using BrouwerWebApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BrouwerWebApp.Controllers
{
    public class HomeController(IBrouwerRepository repository) : Controller
    {
        //readonly IBrouwerRepository repository;

        //Met dit project combineren we normale controllers MVC met REST controllers
        //Deze controller is een MVC controller owv views


        public async Task<IActionResult> Index()
        {
            return View(await repository.FindAllAsync());
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
}
