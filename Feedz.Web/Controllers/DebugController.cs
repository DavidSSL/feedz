using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Feedz.Data.Models;
using Feedz.Worker.Jobs;

namespace Feedz.Web.Controllers;

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

    public string TestFeedAsync([FromQuery(Name = "uri")] string feedUriString)
    {
        if (feedUriString == null) throw new Exception("Missing uri parameter");
        TestFeed.Schedule(new Uri(feedUriString));
        return "ok";
    }

    public Feedz.Data.Models.Feed TestFeedSync([FromQuery(Name = "uri")] string feedUriString)
    {
        if (feedUriString == null) throw new Exception("Missing uri parameter");
        return TestFeed.Run(new Uri(feedUriString));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

