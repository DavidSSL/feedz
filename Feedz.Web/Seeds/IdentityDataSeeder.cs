using Feedz.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Feedz.Web.Settings;

public class IdentityDataSeeder
{
    private static string adminEmail = "admin@example.com";
    private static string adminPassword = "secret";

    private UserManager<ApplicationUser> _userManager;
    private RoleManager<ApplicationRole> _roleManager;

    private ILogger<IdentityDataSeeder> _logger;

    public IdentityDataSeeder(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        ILogger<IdentityDataSeeder> logger
    )
    {
        _logger = logger;
        _userManager = userManager;
        _roleManager = roleManager;
        _logger.LogInformation("Initialized IdentityDataSeeder");
    }

    public async Task SeedAsync()
    {
        _logger.LogInformation("Running IdentityDataSeed");
        var needSeed = await IsSeedNeeded();
        if (!needSeed)
        {
            _logger.LogInformation("No seeding needed");
            return;
        }
        var administratorRole = new ApplicationRole
        {
            Id = Guid.NewGuid(),
            Name = "Administrator",
            NormalizedName = "ADMINISTRATOR"
        };
        await CreateRoleAsync(administratorRole);

        var administratorUser = new ApplicationUser()
        {
            Id = Guid.NewGuid(),
            UserName = adminEmail,
            NormalizedUserName = adminEmail.ToUpper(),
            Email = adminEmail,
            NormalizedEmail = adminEmail.ToUpper(),
            EmailConfirmed = true
        };

        await CreateUserAsync(administratorUser, adminPassword);

        await GrantRoleToUser("Administrator", adminEmail);

        _logger.LogInformation("Completed IdentityDataSeeder");
    }

    private async Task<bool> IsSeedNeeded()
    {
        var noRoles = await _roleManager.Roles.CountAsync() == 0;
        var noUsers = await _userManager.Users.CountAsync() == 0;
        return noRoles && noUsers;
    }

    private async Task CreateRoleAsync(ApplicationRole role)
    {
        var exits = await _roleManager.RoleExistsAsync(role.Name);
        if (!exits)
        {
            _logger.LogInformation("Creating role {role}", role.NormalizedName);
            await _roleManager.CreateAsync(role);
        }
    }

    private async Task CreateUserAsync(ApplicationUser user, string password)
    {
        var currentUser = await _userManager.FindByEmailAsync(user.Email);
        if (currentUser == null)
        {
            _logger.LogInformation("Creating user {email}", user.Email);
            await _userManager.CreateAsync(user, password);
        }
    }

    private async Task GrantRoleToUser(string roleName, string userEmail)
    {
        var user = await _userManager.FindByEmailAsync(userEmail);
        var alreadyAssigned = await _userManager.IsInRoleAsync(user, roleName);
        if (!alreadyAssigned)
        {
            _logger.LogInformation("Granting role {role} to {email}", roleName, userEmail);
            await _userManager.AddToRoleAsync(user, roleName);
        }
    }
}