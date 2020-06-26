using AllInterfaces;

using Domain;

using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using ModelLayer;
using System;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Identity;
using ModelLayer.ViewModelLayer.ViewSetting;
using AllInterfaces.Interface;
using IdentitySample.ViewModels.Role;
using System.Security.Claims;

namespace AllServises.Add {
    public class AddService {
        private readonly IUnitOfWork<Setting> _GetWork‍;
        private readonly IMemoryCache _MemoryCache;
        private readonly IUtilities _Utilities;
        private readonly RoleManager<IdentityRole> _RoleManager;

        public AddService(
            IUnitOfWork<Setting> getWork,
            IMemoryCache memoryCache,
            RoleManager<IdentityRole> roleManager,
            IUtilities utilities) {
            _MemoryCache = memoryCache;
            _GetWork = getWork;
            _RoleManager = roleManager;
            _Utilities = utilities;
        }
        public async Task<IList<Setting>> GetSiteSetting() {
            return await _GetWork.Repository.All();
        }
        public RoleValidationGuidViewModel GetRoleValidationGuid() {
            var model = new RoleValidationGuidViewModel(_GetWork.Repository.where(t => t.Key == "RoleValidationGuid").FirstOrDefault()?.Value, _GetWork.Repository.where(t => t.Key == "RoleValidationGuid").FirstOrDefault()?.LastTimeChanged);
            return model;
        }
        public RoleValidationGuidViewModel GetRoleValidationGuid(RoleValidationGuidViewModel viewModel) {
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
            return viewModel;
        }
        public List<RoleIndexViewModel> GetRoleIndex() {
            var allRole = _RoleManager.Roles.ToList();
            var model = new List<RoleIndexViewModel>();
            foreach (var role in allRole) model.Add(new RoleIndexViewModel(role.Name, role.Id));
            return model;
        }
        public CreateRoleViewModel GetCreatRole() {
            var allMvcNames = _MemoryCache.GetOrCreate("ActionAndControllerNamesList", p => {
                p.AbsoluteExpiration = DateTimeOffset.FromUnixTimeMilliseconds(86400000);
                return _Utilities.ActionAndControllerNamesList();
            });
            var model = new CreateRoleViewModel(allMvcNames);
            return model;
        }
        public async Task<CreateRoleViewModel> GetCreatRole(CreateRoleViewModel viewModel) {
            var role = new IdentityRole(viewModel.RoleName);
            var result = await _RoleManager.CreateAsync(role);
            if (result.Succeeded) {
                var requestRole = viewModel.ActionAndControllerNames.Where(c => c.IsSelected).ToList();
                foreach (var item in requestRole) await _RoleManager.AddClaimAsync(role, new Claim($"{(string.IsNullOrEmpty(item.AreaName) ? "NoArea" : item.AreaName)}|{item.ControllerName}|{item.ActionName}", true.ToString()));
            }
            foreach (var Error in result.Errors) {
                _ = Error.Description;
            }
            return viewModel;
        }
    }
}
