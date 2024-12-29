using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HW_12_25Auth_Autorize.Models
{
    public class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
            {
                var dateOfBirth = DateTime.Parse(context.User.FindFirstValue(ClaimTypes.DateOfBirth));
                var age = DateTime.Today.Year - dateOfBirth.Year;

                if(age >= requirement.MinimumAge)
                {
                    context.Succeed(requirement);
                }
            }
            return Task.CompletedTask;
        }
    }
}
