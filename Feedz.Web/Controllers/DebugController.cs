using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Feedz.Data.Models;
using Feedz.Worker.Jobs;
using Microsoft.AspNetCore.Authorization;

namespace Feedz.Web.Controllers;

[Authorize(Roles = "Administrator")]
public class DebugController : Controller
{
    private readonly ILogger<DebugController> _logger;

    public DebugController(ILogger<DebugController> logger)
    {
        _logger = logger;
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

    public ViewResult TestFeedSync([FromQuery(Name = "feedUri")] string feedUriString)
    {
        if (feedUriString == null) throw new Exception("Missing feedUri parameter");
        var feedData = TestFeed.Run(new Uri(feedUriString));
        return View("TestFeed", feedData);
    }
 
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

