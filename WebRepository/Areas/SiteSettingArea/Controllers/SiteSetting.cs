using System.Threading.Tasks;

using AllServises.Add;

using Microsoft.AspNetCore.Mvc;

using ModelLayer;

namespace WebRepository.Areas.SiteSettingArea.Controllers {
    [Area("SiteSettingArea")]
    public class SiteSetting : Controller {

        private readonly AddService _Service;

        public SiteSetting(AddService service) {
            _Service = service;
        }

        public async Task<IActionResult> Index() {
            return View(await _Service.GetSiteSetting());
        }

        [HttpGet]
        public IActionResult RoleValidationGuid() {
            return View(_Service.GetRoleValidationGuid());
        }
        [HttpPost]
        public IActionResult RoleValidationGuid(RoleValidationGuidViewModel model) {
            _Service.GetRoleValidationGuid(model);
            return RedirectToAction(nameof(Index));
        }
    }
}
