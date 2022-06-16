using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Feedz.Data.Models;
using Feedz.Worker.Jobs;

namespace Feedz.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    static Uri TEST_URI = new Uri("https://www.clubic.com/feed/news.rss");

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public string Ping()
    {
        PingJob.Schedule();
        return "ok";
    }

    public string Test()
    {
        TestFeed.Schedule(TEST_URI);
        return "ok";
    }

    public Feedz.Data.Models.Feed TestSync()
    {
        return TestFeed.Run(TEST_URI);
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

