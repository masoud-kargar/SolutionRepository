using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer.ViewModelLayer.ViewSetting {
    public class RoleIndexViewModel {
        public RoleIndexViewModel(string name, string id) {
            RoleName = name;
            RoleId = id;
        }

        public string RoleId { get; set; }
        public string RoleName { get; set; }
    }
}