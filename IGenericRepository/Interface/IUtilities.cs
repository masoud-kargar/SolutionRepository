using IdentitySample.ViewModels.Role;

using System;
using System.Collections.Generic;
using System.Text;

namespace AllInterfaces.Interface {
    public interface IUtilities {
        public IList<ActionAndControllerName> ActionAndControllerNamesList();
        public IList<string> GetAllAreasNames();
        public string DataBaseRoleValidationGuid();
    }
}
