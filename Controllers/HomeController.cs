using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FRWeb.Models;
using System.Collections.Generic;
using RiggVar.FR;

namespace FRWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Info()
        {
            //List<string> sl = new List<string>();
            //Dummy.ApiCol.GetApiList(sl, ApiControllerEnum.Delphi, false);

            ViewData.Model = Dummy.ApiCol.GetLinqList(); //Dummy.ApiCol.GetItems(ApiControllerEnum.Delphi);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
