using CompanyEmployees.Entities.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

public class RoleToClaimMigration
{
    private readonly UserManager<User> _userManager;

    public RoleToClaimMigration(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task MigrateAsync()
    {
        var users = _userManager.Users.ToList();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                var roleClaims = await GetClaimsForRole(role);

                foreach (var claim in roleClaims)
                {
                    await _userManager.AddClaimAsync(user, claim);
                }

                await _userManager.RemoveFromRoleAsync(user, role);
            }
        }
    }

    private async Task<List<Claim>> GetClaimsForRole(string roleName)
    {
        var claims = new List<Claim>();

        // Define your role-to-claim mappings here
        switch (roleName)
        {
            case "Administrator":
                claims.Add(new Claim("Permission", Permissions.Products.View));
                claims.Add(new Claim("Permission", Permissions.Products.Create));
                claims.Add(new Claim("Permission", Permissions.Products.Edit));
                claims.Add(new Claim("Permission", Permissions.Products.Delete));
                // Add more claims as needed
                break;
            case "User":
                claims.Add(new Claim("Permission", Permissions.Products.View));
                // Add more claims as needed
                break;
                // Add more roles as needed
        }

        return claims;
    }
}