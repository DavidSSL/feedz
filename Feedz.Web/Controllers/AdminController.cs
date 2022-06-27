using Feedz.Data.Database;
using Feedz.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Feedz.Web.Controllers;

[Authorize(Roles = "Administrator")]
public class AdminController : Controller
{
    private UserManager<ApplicationUser> _userManager;
    private RoleManager<ApplicationRole> _roleManager;
    private readonly ApplicationDbContext _db;

    public AdminController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, ApplicationDbContext db)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.UsersCount = await _userManager.Users.CountAsync();
        ViewBag.RolesCount = await _roleManager.Roles.CountAsync();
        ViewBag.FeedsCount = await _db.Feeds.CountAsync();
        ViewBag.FeedEntriesCount = await _db.FeedEntries.CountAsync();
        ViewBag.FeedSubscriptionsCount = await _db.FeedSubscriptions.CountAsync();
        ViewBag.FeedEntryUserStates = await _db.FeedEntryUserStates.CountAsync();
        return View();
    }
}