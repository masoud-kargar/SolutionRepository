using AllInterfaces;

using Domain;

using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using ModelLayer;
using System;
using Microsoft.Extensions.Caching.Memory;

namespace AllServises.Add {
    public class AddService {
        private readonly IUnitOfWork<Setting> _GetWork‍;
        private readonly IMemoryCache _MemoryCache;

        public AddService(IUnitOfWork<Setting> getWork, IMemoryCache memoryCache) {
            _MemoryCache = memoryCache;
            _GetWork = getWork;
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
    }
}
