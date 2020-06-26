using System.Collections.Generic;

namespace ModelLayer.ViewModelLayer.Category {
    public class SubCategoryViewModel {
        #region Canstractor
        public SubCategoryViewModel() {

        }
        public SubCategoryViewModel(string name,string icon, IList<ParenCategoryViewModel> parent) {
            SubName = name;
            Icon = icon;
            parent = Parent;
        }

        public SubCategoryViewModel(string c,string icon, List<ParenCategoryViewModel> parent) {
            SubName = c;
            Icon = icon;
            Parent = parent;
        }
        #endregion
        public string SubName { get; set; }

        public string Icon { get; set; }

        public List<ParenCategoryViewModel> Parent { get; set; }
    }
}
