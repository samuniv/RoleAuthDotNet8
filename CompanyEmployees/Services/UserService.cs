using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Services
{
    public class UserService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task AssignPermissionsToUser(string userId, IEnumerable<string> permissions)
        {
            var user = await _userManager.FindByIdAsync(userId);

            // Remove existing permission claims
            var existingClaims = await _userManager.GetClaimsAsync(user);
            var permissionClaims = existingClaims.Where(c => c.Type == "Permission");
            await _userManager.RemoveClaimsAsync(user, permissionClaims);

            // Add new permission claims
            var newClaims = permissions.Select(p => new Claim("Permission", p));
            await _userManager.AddClaimsAsync(user, newClaims);
        }
    }
}