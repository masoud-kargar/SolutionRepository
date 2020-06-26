using System;

namespace ModelLayer {


    public class RoleValidationGuidViewModel {
        #region Constructor
        public RoleValidationGuidViewModel() {
        }

        public RoleValidationGuidViewModel(string Value, DateTime? LastTimeChanged) {
            if (string.IsNullOrEmpty(Value)) {
                throw new ArgumentException("message", nameof(Value));
            }

            if (LastTimeChanged is null) {
                throw new ArgumentNullException(nameof(LastTimeChanged));
            }
        }
        #endregion
        public string Value { get; set; }
        public DateTime? LastTimeChanged { get; set; }
    }
}
