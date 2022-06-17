using Feedz.Data.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Feedz.Web.Settings;

public class IdentityDataSeeder
{
    private static string adminEmail = "admin@example.com";
    private static string adminPassword = "secret";

    private UserManager<IdentityUser> _userManager;
    private RoleManager<IdentityRole> _roleManager;

    private ILogger<IdentityDataSeeder> _logger;

    public IdentityDataSeeder(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        ILogger<IdentityDataSeeder> logger
    )
    {
        _logger = logger;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task SeedAsync()
    {
        _logger.LogInformation("Running IdentityDataSeed");
        var administratorRole = new IdentityRole
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Administrator",
            NormalizedName = "ADMINISTRATOR"
        };
        await CreateRoleAsync(administratorRole);

        var administratorUser = new IdentityUser
        {
            Id = Guid.NewGuid().ToString(),
            UserName = adminEmail,
            NormalizedUserName = adminEmail.ToUpper(),
            Email = adminEmail,
            NormalizedEmail = adminEmail.ToUpper(),
            EmailConfirmed = true
        };

        await CreateUserAsync(administratorUser, adminPassword);

        await GrantRoleToUser("Administrator", adminEmail);

    }

    private async Task CreateRoleAsync(IdentityRole role)
    {
        var exits = await _roleManager.RoleExistsAsync(role.Name);
        if (!exits)
        {
            _logger.LogInformation("Creating role {role}", role.NormalizedName);
            await _roleManager.CreateAsync(role);
        }
    }

    private async Task CreateUserAsync(IdentityUser user, string password)
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