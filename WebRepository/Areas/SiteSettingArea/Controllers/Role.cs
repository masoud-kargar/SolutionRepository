using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using AllInterfaces;
using AllInterfaces.Interface;

using Domain;

using IdentitySample.ViewModels.Role;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

using ModelLayer.ViewModelLayer.ViewSetting;

namespace WebRepository.Areas.SiteSettingArea.Controllers {
    [Area("SiteSettingArea")]
    public class Role : Controller {
        private readonly IUnitOfWork<Setting> _GetWork‍;
        private readonly IMemoryCache _MemoryCache;
        private readonly IUtilities _Utilities;
        private readonly RoleManager<IdentityRole> _RoleManager;
        public Role(
            IUnitOfWork<Setting> getWork,
            IMemoryCache memoryCache,
            RoleManager<IdentityRole> roleManager,
            IUtilities utilities) {
            _MemoryCache = memoryCache;
            _GetWork = getWork;
            _RoleManager = roleManager;
            _Utilities = utilities;
        }
        public IActionResult Index() {
            var allRole = _RoleManager.Roles.ToList();
            var model = new List<RoleIndexViewModel>();
            foreach (var role in allRole) model.Add(new RoleIndexViewModel(role.Name, role.Id));
            return View(model);
        }
        public IActionResult CreateRole() {
            var allMvcNames = _MemoryCache.GetOrCreate("ActionAndControllerNamesList", p => {
                p.AbsoluteExpiration = DateTimeOffset.FromUnixTimeMilliseconds(86400000);
                return _Utilities.ActionAndControllerNamesList();
            });
            var model = new CreateRoleViewModel(allMvcNames);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel viewModel) {
            var role = new IdentityRole(viewModel.RoleName);
            var result = await _RoleManager.CreateAsync(role);
            if (result.Succeeded) {
                var requestRole = viewModel.ActionAndControllerNames.Where(c => c.IsSelected).ToList();
                foreach (var item in requestRole) await _RoleManager.AddClaimAsync(role, new Claim($"{(string.IsNullOrEmpty(item.AreaName) ? "NoArea" : item.AreaName)}|{item.ControllerName}|{item.ActionName}", true.ToString()));
                return RedirectToAction(nameof(Index));
            }
            foreach (var Error in result.Errors) {
                ModelState.AddModelError("", Error.Description);
            }
            return View(viewModel);
        }

    }
}
