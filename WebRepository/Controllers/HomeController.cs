using System.Diagnostics;
using System.Threading.Tasks;

using Domain;

using IGenericRepository.Interface;

using Microsoft.AspNetCore.Mvc;

using WebRepository.Models;

namespace WebRepository.Controllers {
    public class HomeController : Controller {
        private readonly IUnitOfWork<Person> _unitOfWork;

        public HomeController(IUnitOfWork<Person> unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index() {
            var list = await _unitOfWork.Repository.All();
            return View(list);
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
