using System.Linq;
using System.Threading.Tasks;

using AllServises.Claims;

using ModelLayer.ViewModelLayer.Claims;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace WebRepository.Areas.ClaimsArea.Controllers {
    [Area("ClaimsArea")]
    //[Authorize(Roles = "Owner")]
    //[Authorize(Roles = "Admin")]
    public class ClaimsController : Controller {
        private readonly UserManager<IdentityUser> _UserManager;
        private readonly RoleManager<IdentityRole> _RoleManager;

        public ClaimsController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager) {
            _UserManager = userManager;
            _RoleManager = roleManager;
        }

        [Authorize(Policy = "ClaimsAdmin")]
        [HttpGet]
        public async Task<IActionResult> AddUserToClaim(string id) {
            if (ModelState.IsValid) {
                if (string.IsNullOrEmpty(id)) return NotFound();
                var user = await _UserManager.FindByIdAsync(id);
                if (user == null) return NotFound();
                var allClaim = ClaimStore.AllClaims;
                var userClaims = await _UserManager.GetClaimsAsync(user);
                var validClaims = allClaim.Where(c => userClaims.All(x => x.Type != c.Type))
                    .Select(c => new ClaimViewModel(c.Type)).ToList();
                var model = new A_R_ClaimsViewModel(id, validClaims);
                return View(model);
            }
            return View();
        }

        [Authorize(Policy = "ClaimsAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUserToClaim(A_R_ClaimsViewModel model) {
            if (ModelState.IsValid) {
                if (model == null) return NotFound();
                var user = await _UserManager.FindByIdAsync(model.UserId);
                if (user == null) return NotFound();
                var requestClaim = model.UserClaims.Where(r => r.IsSelected).Select(u => new Claim(u.ClaimType, true.ToString()));
                var result = await _UserManager.AddClaimsAsync(user, requestClaim);
                if (result.Succeeded) return RedirectToAction(nameof(Index));
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
                return View(model);
            }
            return View();
        }

        [Authorize(Policy = "ClaimsAdmin")]
        [HttpGet]
        public async Task<IActionResult> RemoveUserFromClaim(string id) {
            if (ModelState.IsValid) {
                if (string.IsNullOrEmpty(id)) return NotFound();
                var user = await _UserManager.FindByIdAsync(id);
                if (user == null) return NotFound();

                var userClaims = await _UserManager.GetClaimsAsync(user);
                var validClaims = userClaims.Select(c => new ClaimViewModel(c.Type)).ToList();
                var model = new A_R_ClaimsViewModel(id, validClaims);
                return View(model);
            }
            return View();
        }

        [Authorize(Policy = "ClaimsAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveUserFromClaim(A_R_ClaimsViewModel model) {
            if (ModelState.IsValid) {
                if (model == null) return NotFound();
                var user = await _UserManager.FindByIdAsync(model.UserId);
                if (user == null) return NotFound();
                var requestClaim = model.UserClaims.Where(r => r.IsSelected).Select(u => new Claim(u.ClaimType, true.ToString()));
                var result = await _UserManager.RemoveClaimsAsync(user, requestClaim);
                if (result.Succeeded) return RedirectToAction(nameof(Index));
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
                return View(model);
            }
            return View();
        }
    } 
}
