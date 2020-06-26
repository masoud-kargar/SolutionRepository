using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebRepository.Areas.IdentityArea.Controllers {
    [Area("IdentityArea")]
    //[Authorize(Roles = "Admin")]
    //[Authorize(Roles = "Owner")]
    public class ManageRole : Controller {
        private readonly RoleManager<IdentityRole> _roleManager;

        public ManageRole(RoleManager<IdentityRole> roleManager) {
            _roleManager = roleManager;
        }

        [Authorize(Policy = "RoleListPolicy")]
        public IActionResult Index() {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

        [Authorize(Policy = "RoleAddPolicy")]
        [HttpGet]
        public IActionResult AddRole() {
            return View();
        }

        [Authorize(Policy = "RoleAddPolicy")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(string name) {
            if (string.IsNullOrEmpty(name)) return NotFound();
            var role = new IdentityRole(name);
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded) return RedirectToAction("Index");

            foreach (var error in result.Errors) {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(role);
        }

        [Authorize(Policy = "RoleEditPolicy")]
        [HttpGet]
        public async Task<IActionResult> EditRole(string id) {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var role = await _roleManager.FindByIdAsync(id);

            if (role == null) return NotFound();

            return View(role);
        }

        [Authorize(Policy = "RoleEditPolicy")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRole(string id, string name) {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name)) return NotFound();

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();
            role.Name = name;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded) return RedirectToAction("Index");

            foreach (var error in result.Errors) {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(role);
        }

        [Authorize(Policy = "RoleDeletePolicy")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRole(string id) {
            if (string.IsNullOrEmpty(id)) return NotFound();
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();
            await _roleManager.DeleteAsync(role);

            return RedirectToAction("Index");
        }

    }
}