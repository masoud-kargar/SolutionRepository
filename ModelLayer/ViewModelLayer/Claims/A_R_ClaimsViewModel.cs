using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer.ViewModelLayer.Claims {
    public class A_R_ClaimsViewModel {
        #region Constractor
        public A_R_ClaimsViewModel() {
            UserClaims = new List<ClaimViewModel>();
        }
        public A_R_ClaimsViewModel(string userId, IList<ClaimViewModel> userClaims) {
            UserId = userId;
            UserClaims = userClaims;
        }
        #endregion
        public string UserId { get; set; }
        public IList<ClaimViewModel> UserClaims { get; set; }
    }
}
