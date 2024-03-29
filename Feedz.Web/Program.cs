﻿using Microsoft.EntityFrameworkCore;
using Feedz.Data.Database;
using Feedz.Data.Models;
using Feedz.Web.Seeds;
using Feedz.Web.Services;
using Hangfire;
using Hangfire.PostgreSql;
using Feedz.Web.Settings;
var builder = WebApplication.CreateBuilder(args);
var hangfireConnectionString = builder.Configuration.GetConnectionString("HangfireConnection");

// Add services to the container.

// Database configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString, b => b.MigrationsAssembly("Feedz.Data")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Authentication provider configuration
builder.Services.AddDefaultIdentity<ApplicationUser>(options => Authentication.ConfigureIdentity(options))
    .AddRoles<ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IdentityDataSeeder>();
builder.Services.AddHostedService<SetupIdentityDataSeeder>();

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
    //app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

// Start the application
app.Run();


