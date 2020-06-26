using System.Threading.Tasks;

using AllServises.Add;

using IdentitySample.ViewModels.Role;

using Microsoft.AspNetCore.Mvc;

namespace WebRepository.Areas.SiteSettingArea.Controllers {
    [Area("SiteSettingArea")]
    public class Role : Controller {
        private readonly AddService _Service;
        public Role(AddService service) => _Service = service;
        public IActionResult Index() => View(_Service.GetRoleIndex());
        public IActionResult CreateRole() => View(_Service.GetCreatRole());
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel viewModel) => View(await _Service.GetCreatRole(viewModel));
    }
}
