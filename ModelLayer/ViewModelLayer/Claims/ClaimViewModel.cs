namespace ModelLayer.ViewModelLayer.Claims {
    public class ClaimViewModel {
        #region Constractor
        public ClaimViewModel() {
        }
        public ClaimViewModel(string claimType) {
            ClaimType = claimType;
        }
        #endregion
        public string ClaimType { get; set; }
        public bool IsSelected { get; set; }
    }
}
