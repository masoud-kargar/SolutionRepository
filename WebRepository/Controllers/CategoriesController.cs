using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain;
using IGenericRepository.Interface;
using System.Runtime.CompilerServices;

namespace WebRepository.Controllers {
    public class CategoriesController : Controller {
        private readonly IUnitOfWork<Category> _unitOfWork;
        public CategoriesController(IUnitOfWork<Category> unitOfWork) => _unitOfWork = unitOfWork;
        public async Task<IActionResult> Index() {
            var List = _unitOfWork.Repository.All();
            return View(await List);
        }
        public async Task<IActionResult> Details(Guid? id) {
            if (id == null) {
                return NotFound();
            }
            try {
                var category = await _unitOfWork.Repository.GetById((Guid)id);
                if (category == null) {
                    return NotFound();
                }
                return View(category);
            } catch (Exception) {
                return RedirectToAction(nameof(Index));
                throw;
            } finally {
                _unitOfWork.Dispose();
            }
        }
        public IActionResult CreateSub() {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSub([Bind("Name,Icon,Id")] Category category) {
            if (ModelState.IsValid) {
                try {
                    await _unitOfWork.Repository.Add(category);
                    await _unitOfWork.Commit();
                    return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _unitOfWork.Repository.Add(category)) });
                } catch (Exception) {
                    return NotFound();
                    throw;
                } finally {
                    _unitOfWork.Dispose();
                }
            }
            ViewData["ParentId"] = new SelectList(_unitOfWork.Repository.where(x => x.ParentId == null), "Id", "Name", category.ParentId);
            return Json(new { IsValid = false, html = Helper.RenderRazorViewToString(this, "Index", category) });
        }
        public async Task<IActionResult> Edit(Guid? id) {
            if (id == null) {
                return NotFound();
            }
            try {
                var category = await _unitOfWork.Repository.GetById((Guid)id);
                ViewData["ParentId"] = new SelectList(_unitOfWork.Repository.where(x => x.ParentId == null), "Id", "Name", category.ParentId);
                if (category == null) {
                    return NotFound();
                }
                return View(category);
            } catch (Exception) {
                return RedirectToAction(nameof(Index));
                throw;
            } finally {
                _unitOfWork.Dispose();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,ParentId,Icon,Id")] Category category) {
            if (id != category.Id) {
                return NotFound();
            }
            if (ModelState.IsValid) {
                try {
                    _unitOfWork.Repository.Update(category);
                    await _unitOfWork.Commit();
                } catch (DbUpdateConcurrencyException) {
                    if (!_unitOfWork.Repository.where(category.Id)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return Json(new { IsValid = true, html = "" });
            }
            ViewData["ParentId"] = new SelectList(_unitOfWork.Repository.where(x => x.ParentId == null), "Id", "Name", category.ParentId);
            _unitOfWork.Dispose();
            return View(category);
        }
        public async Task<IActionResult> Delete(Guid? id) {
            if (id == null) {
                return NotFound();
            }
            try {
                var category = await _unitOfWork.Repository.GetById((Guid)id);
                if (category == null) {
                    return NotFound();
                }
                return View(category);
            } catch (Exception) {
                return RedirectToAction(nameof(Index));
                throw;
            } finally {
                _unitOfWork.Dispose();
            }
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id) {
            try {
                if (id == null) {
                    return NotFound();
                }
                var Category = await _unitOfWork.Repository.GetById(id);
                _unitOfWork.Repository.Delete(Category);
                return RedirectToAction(nameof(Index));
            } catch (Exception) {
                return NotFound();
                throw;
            } finally {
                _unitOfWork.Dispose();
            }
        }
    }
}
