using Microsoft.AspNetCore.Authorization;

namespace HW_12_25Auth_Autorize.Models
{
    public class MinimumAgeRequirement : IAuthorizationRequirement
    {
        public int MinimumAge { get; set; }
        public MinimumAgeRequirement(int minage)
        {
            MinimumAge = minage;
        }
    }
}
