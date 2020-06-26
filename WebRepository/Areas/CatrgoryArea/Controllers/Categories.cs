using AllInterfaces;

using Domain;

using Microsoft.AspNetCore.Mvc;

using ModelLayer.ViewModelLayer.Category;

using System.Linq;
using System.Threading.Tasks;

namespace WebRepository.Areas.CatrgoryArea.Controllers {
    public class Categories : Controller {
        private readonly IUnitOfWork<Category> _Unit;
        public Categories(IUnitOfWork<Category> unit) {
            _Unit = unit;
        }
        public async Task<IActionResult> IndexAsync() {
            //var all = await _Unit.Repository.All();
            //var icon =  _Unit.Repository.where(x => x.Icon != null);
            //var parent =  _Unit.Repository.where(x => x.ParentId != null);
            //var validAll =  all.Where(c => all.All(x => c.ParentId == null))
            //        .Select(c => new SubCategoryViewModel(c,icon, parent)).ToList();
            return View();
        }
    }
}
