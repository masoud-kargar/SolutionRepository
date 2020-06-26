using System;
using System.Linq;
using System.Threading.Tasks;
using AllInterfaces;
using AllInterfaces.Interface;
using AllServises.Add;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ModelLayer;

namespace WebRepository.Areas.SiteSettingArea.Controllers {
    [Area("SiteSettingArea")]
    public class SiteSetting : Controller {

        private readonly AllInterfaces.IUnitOfWork<Setting> _GetWork‍;
        private readonly IMemoryCache _MemoryCache;
        private readonly IUtilities _Utilities;
        private readonly RoleManager<IdentityRole> _RoleManager;
        public SiteSetting(
            IUnitOfWork<Setting> getWork,
            IMemoryCache memoryCache,
            RoleManager<IdentityRole> roleManager,
            IUtilities utilities) {
            _MemoryCache = memoryCache;
            _GetWork = getWork;
            _RoleManager = roleManager;
            _Utilities = utilities;
        }

        public async Task<IActionResult> Index() {
            return View(await _GetWork.Repository.All());
        }

        [HttpGet]
        public IActionResult RoleValidationGuid() {
            var model = new RoleValidationGuidViewModel(_GetWork.Repository.where(t => t.Key == "RoleValidationGuid").FirstOrDefault()?.Value, _GetWork.Repository.where(t => t.Key == "RoleValidationGuid").FirstOrDefault()?.LastTimeChanged);
            return View(model);
        }
        [HttpPost]
        public IActionResult RoleValidationGuid(RoleValidationGuidViewModel viewModel) {
            if (_GetWork.Repository.where(t => t.Key == "RoleValidationGuid").FirstOrDefault() == null) {
                _GetWork.Repository.Add(new Setting() {
                    Key = "RoleValidationGuid",
                    Value = Guid.NewGuid().ToString(),
                    LastTimeChanged = DateTime.Now
                });
            } else {
                _GetWork.Repository.where(t => t.Key == "RoleValidationGuid").FirstOrDefault().Value = Guid.NewGuid().ToString();
                _GetWork.Repository.where(t => t.Key == "RoleValidationGuid").FirstOrDefault().LastTimeChanged = DateTime.Now;
                _GetWork.Repository.Update(_GetWork.Repository.where(t => t.Key == "RoleValidationGuid").FirstOrDefault());
            }
            _GetWork.Commit();
            _GetWork.Dispose();
            _MemoryCache.Remove("RoleValidationGuid");
            return RedirectToAction(nameof(Index));
        }
    }
}
