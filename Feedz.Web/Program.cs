using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Feedz.Data.Database;
using Hangfire;
using Hangfire.PostgreSql;
using Npgsql;
using Feedz.Web.Settings;

var builder = WebApplication.CreateBuilder(args);
var hangfireConnectionString = builder.Configuration.GetConnectionString("HangfireConnection");

// Add services to the container.

// Database configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Authentication provider configuration
builder.Services.AddDefaultIdentity<IdentityUser>(options => Authentication.ConfigureIdentity(options))
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

//builder.Services.AddHangfire(configuration => configuration
//        .UseSimpleAssemblyNameTypeSerializer()
//        .UseRecommendedSerializerSettings()
//        .UsePostgreSqlStorage(hangfireConnectionString));

// Hangfire job storage
JobStorage.Current = new PostgreSqlStorage(hangfireConnectionString);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

