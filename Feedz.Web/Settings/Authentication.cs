using Microsoft.AspNetCore.Identity;

namespace Feedz.Web.Settings
{
    public static class Authentication
    {
        public static IdentityOptions ConfigureIdentity(IdentityOptions options)
        {
#if DEBUG
            return ConfigureDevelopmentIdentity(options);
#else
            return ConfigureProductionIdentity(options);
#endif
        }

        public static IdentityOptions ConfigureDevelopmentIdentity(IdentityOptions options)
        {
            // Password policy
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 0;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;

            // Signin policy
            options.SignIn.RequireConfirmedAccount = false;
            options.SignIn.RequireConfirmedEmail = false;

            // Lockout policy
            options.Lockout.AllowedForNewUsers = false;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(1);
            options.Lockout.MaxFailedAccessAttempts = 10;

            // User policy
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = true;

            return options;

        }
        public static IdentityOptions ConfigureProductionIdentity(IdentityOptions options)
        {
            // Password policy
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;

            // Signin policy
            options.SignIn.RequireConfirmedAccount = false;
            options.SignIn.RequireConfirmedEmail = true;

            // Lockout policy
            options.Lockout.AllowedForNewUsers = true;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;

            // User policy
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = true;

            return options;
        }
    }
}