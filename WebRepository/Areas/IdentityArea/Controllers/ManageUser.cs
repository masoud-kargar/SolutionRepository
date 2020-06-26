using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using IdentitySample.ViewModels.ManageUser;
using System;
using Microsoft.AspNetCore.Authorization;

namespace WebRepository.Areas.IdentityArea.Controllers {
    [Area("IdentityArea")]
    //[Authorize(Roles = "Admin")]
    //[Authorize(Roles = "Owner")]
    public class ManageUser : Controller {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ManageUser(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager) {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        //[Authorize(Policy = "UserListPolicy")]
        [HttpGet]
        public IActionResult Index() {
            var model = _userManager.Users
                .Select(u => new IndexViewModel() { Id = u.Id, UserName = u.UserName, Email = u.Email }).ToList();
            return View(model);
        }

        //[Authorize(Policy = "UserAddPolicy")]
        [HttpGet]
        public async Task<IActionResult> AddUserToRole(string id) {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            var roles = _roleManager.Roles.AsNoTracking().Select(r => r.Name).ToList();
            var userRoles = await _userManager.GetRolesAsync(user);
            var validRoles = roles.Where(r => !userRoles.Contains(r)).Select(r=>new UserRolesViewModel(r)).ToList();
            var model = new AddUserToRoleViewModel(id, validRoles);
            return View(model);
        }

        //[Authorize(Policy = "UserAddPolicy")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUserToRole(AddUserToRoleViewModel model) {
            if (model == null) return NotFound();
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null) return NotFound();
            var requestRoles = model.UserRoles.Where(r => r.IsSelected)
                .Select(u => u.RoleName)
                .ToList();
            var result = await _userManager.AddToRolesAsync(user, requestRoles);

            if (result.Succeeded) return RedirectToAction("index");

            foreach (var error in result.Errors) {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        //[Authorize(Policy = "UserEditPolicy")]
        [HttpGet]
        public async Task<IActionResult> EditUser(string id) {
            if (string.IsNullOrEmpty(id)) return NotFound();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            return View(user);
        }

        //[Authorize(Policy = "UserEditPolicy")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(string id, string userName) {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(userName)) return NotFound();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            user.UserName = userName;
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded) return RedirectToAction("index");

            foreach (var error in result.Errors) {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(user);
        }

        //[Authorize(Policy = "UserDeletePolicy")]
        [HttpGet]
        public async Task<IActionResult> RemoveUserFromRole(string id) {
            if (string.IsNullOrEmpty(id)) return NotFound();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            var userRoles = await _userManager.GetRolesAsync(user);
            var validRoles = userRoles.Select(r => new UserRolesViewModel(r)).ToList();
            var model = new AddUserToRoleViewModel(id,validRoles);
            return View(model);
        }

        //[Authorize(Policy = "UserDeletePolicy")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveUserFromRole(AddUserToRoleViewModel model) {
            if (model == null) return NotFound();
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null) return NotFound();
            var requestRoles = model.UserRoles.Where(r => r.IsSelected)
                .Select(u => u.RoleName)
                .ToList();
            var result = await _userManager.RemoveFromRolesAsync(user, requestRoles);

            if (result.Succeeded) return RedirectToAction("index");

            foreach (var error in result.Errors) {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        //[Authorize(Policy = "UserDeletePolicy")]
        [HttpGet]
        public async Task<IActionResult> DeleteUser(string id) {
            if (string.IsNullOrEmpty(id)) return NotFound();
            var GetId = await _userManager.FindByIdAsync(id);
            if (GetId == null) return NotFound();
            return View(GetId);
        }

        //[Authorize(Policy = "UserDeletePolicy")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("DeleteUser")]
        public async Task<IActionResult> DeleteUserConfirm(string id) {
            if (string.IsNullOrEmpty(id)) return NotFound();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            await _userManager.DeleteAsync(user);

            return RedirectToAction("Index");
        }
    }
}