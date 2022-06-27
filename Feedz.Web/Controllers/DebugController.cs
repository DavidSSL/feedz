using System.Diagnostics;
using Feedz.Data.Database;
using Microsoft.AspNetCore.Mvc;
using Feedz.Data.Models;
using Feedz.Feed;
using Feedz.Worker.Jobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Feedz.Web.Controllers;

[Authorize(Roles = "Administrator")]
[Route("admin/debug")]
public class DebugController : Controller
{
    private readonly ILogger<DebugController> _logger;
    private readonly ApplicationDbContext _db;

    public DebugController(ILogger<DebugController> logger, ApplicationDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    [Route("")]
    [Route("index")]
    public IActionResult Index()
    {
        return View();
    }

    [Route("ping")]
    public string Ping()
    {
        PingJob.Schedule();
        return "ok";
    }

    [Route("testfeedasync")]
    public string TestFeedAsync([FromQuery(Name = "feedUri")] string feedUriString)
    {
        if (feedUriString == null) throw new Exception("Missing feedUri parameter");
        TestFeed.Schedule(new Uri(feedUriString));
        return "ok";
    }

    [Route("testfeedsync")]
    public async Task<ViewResult> TestFeedSync([FromQuery(Name = "feedUri")] string feedUriString)
    {
        if (feedUriString == null) throw new Exception("Missing feedUri parameter");
        var feedData = TestFeed.Run(new Uri(feedUriString));
        var isRegistered = await _db.Feeds.CountAsync(f => f.Uri == new Uri(feedUriString)) > 1;

        ViewData["isRegistered"] = isRegistered;

        return View("TestFeed", feedData);
    }
}

