using System.Diagnostics;
using Feedz.Data.Database;
using Microsoft.AspNetCore.Mvc;
using Feedz.Data.Models;
using Feedz.Feed.Services;
using Feedz.Worker.Jobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Feedz.Web.Controllers;

[Authorize(Roles = "Administrator")]
public class DebugController : Controller
{
    private readonly ILogger<DebugController> _logger;
    private readonly ApplicationDbContext _db;

    public DebugController(ILogger<DebugController> logger, ApplicationDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public IActionResult Index()
    {
        return View();
    }

    public string Ping()
    {
        PingJob.Schedule();
        return "ok";
    }

    public string TestFeedAsync([FromQuery(Name = "feedUri")] string feedUriString)
    {
        if (feedUriString == null) throw new Exception("Missing feedUri parameter");
        TestFeed.Schedule(new Uri(feedUriString));
        return "ok";
    }

    public async Task<ViewResult> TestFeedSync([FromQuery(Name = "feedUri")] string feedUriString)
    {
        if (feedUriString == null) throw new Exception("Missing feedUri parameter");
        var feedData = TestFeed.Run(new Uri(feedUriString));
        var isRegistered = await _db.Feeds.CountAsync(f => f.Uri == new Uri(feedUriString)) > 1;

        ViewData["isRegistered"] = isRegistered;

        return View("TestFeed", feedData);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

