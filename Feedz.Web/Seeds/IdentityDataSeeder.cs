using Feedz.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Feedz.Web.Seeds;

public class IdentityDataSeeder
{
    private const string AdminEmail = "admin@example.com";
    private const string AdminPassword = "secret";

    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<ApplicationRole> roleManager;

    private readonly ILogger<IdentityDataSeeder> logger;

    public IdentityDataSeeder(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        ILogger<IdentityDataSeeder> logger
    )
    {
        this.logger = logger;
        this.userManager = userManager;
        this.roleManager = roleManager;
        this.logger.LogInformation("Initialized IdentityDataSeeder");
    }

    public async Task SeedAsync()
    {
        logger.LogInformation("Running IdentityDataSeed");
        var needSeed = await IsSeedNeeded();
        if (!needSeed)
        {
            logger.LogInformation("No seeding needed");
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
            UserName = AdminEmail,
            NormalizedUserName = AdminEmail.ToUpper(),
            Email = AdminEmail,
            NormalizedEmail = AdminEmail.ToUpper(),
            EmailConfirmed = true
        };

        await CreateUserAsync(administratorUser, AdminPassword);

        await GrantRoleToUser("Administrator", AdminEmail);

        logger.LogInformation("Completed IdentityDataSeeder");
    }

    private async Task<bool> IsSeedNeeded()
    {
        var noRoles = await roleManager.Roles.CountAsync() == 0;
        var noUsers = await userManager.Users.CountAsync() == 0;
        return noRoles && noUsers;
    }

    private async Task CreateRoleAsync(ApplicationRole role)
    {
        var exists = await roleManager.RoleExistsAsync(role.Name);
        if (!exists)
        {
            logger.LogInformation("Creating role {role}", role.NormalizedName);
            await roleManager.CreateAsync(role);
        }
    }

    private async Task CreateUserAsync(ApplicationUser user, string password)
    {
        var currentUser = await userManager.FindByEmailAsync(user.Email);
        if (currentUser == null)
        {
            logger.LogInformation("Creating user {email}", user.Email);
            await userManager.CreateAsync(user, password);
        }
    }

    private async Task GrantRoleToUser(string roleName, string userEmail)
    {
        var user = await userManager.FindByEmailAsync(userEmail);
        var alreadyAssigned = await userManager.IsInRoleAsync(user, roleName);
        if (!alreadyAssigned)
        {
            logger.LogInformation("Granting role {role} to {email}", roleName, userEmail);
            await userManager.AddToRoleAsync(user, roleName);
        }
    }
}
