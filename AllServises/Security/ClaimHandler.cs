using Microsoft.AspNetCore.Authorization;

using System.Threading.Tasks;

namespace AllServises {
    public class ClaimHandler : AuthorizationHandler<ClaimRequirement> {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClaimRequirement requirement) {
            if (context.User.HasClaim(requirement.ClaimType, requirement.ClaimValue))
                context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
