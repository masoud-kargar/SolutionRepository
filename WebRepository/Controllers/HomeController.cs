using System.Diagnostics;
using System.Threading.Tasks;

using AllInterfaces;

using Domain;

using Microsoft.AspNetCore.Mvc;

using WebRepository.Models;

namespace WebRepository.Controllers {
    public class HomeController : Controller {

        public IActionResult Index() {
            return View();
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
