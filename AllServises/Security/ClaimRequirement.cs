using System;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;

namespace AllServises {
    public class ClaimRequirement : IAuthorizationRequirement {
        public string ClaimType { get; }
        public string ClaimValue { get; }

        public ClaimRequirement(string claimValue, string claimType) {
            ClaimValue = claimValue;
            ClaimType = claimType;
        }
    }
}
