using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;
using Xunit.Abstractions;

namespace Sobczal1.KickBets.ClientApp.Tests;

public class DashboardTest : IDisposable
{
    private readonly ITestOutputHelper _testOutputHelper;

    public DashboardTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        ChromeDriver = new ChromeDriver();
    }

    public ChromeDriver ChromeDriver { get; set; }

    public void Dispose()
    {
        ChromeDriver.Dispose();
    }

    [Fact]
    public void Should_Render10DashboardListItems_WhenEntered()
    {
        ChromeDriver.Navigate().GoToUrl("http://localhost:3000");
        ChromeDriver.Navigate().GoToUrl("http://localhost:3000/fixtures");

        Thread.Sleep(500);

        var items = ChromeDriver.FindElements(By.ClassName("dashboard-list-item-box"));


        var expected = 10;
        var actual = items.Count;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Should_Render20DashboardListItems_WhenScrolledDown()
    {
        ChromeDriver.Navigate().GoToUrl("http://localhost:3000");
        ChromeDriver.Navigate().GoToUrl("http://localhost:3000/fixtures");

        Thread.Sleep(500);

        ((IJavaScriptExecutor) ChromeDriver).ExecuteScript("window.scrollBy(0, 2000)");

        Thread.Sleep(1000);

        var items = ChromeDriver.FindElements(By.ClassName("dashboard-list-item-box"));

        var expected = 20;
        var actual = items.Count;

        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void Should_RedirectToDetails_WhenFirstListItemClicked()
    {
        ChromeDriver.Navigate().GoToUrl("http://localhost:3000/fixtures");

        Thread.Sleep(500);

        var items = ChromeDriver.FindElements(By.ClassName("dashboard-list-item-box"));
        
        Assert.True(items.Count >= 1);
        
        items[0].Click();
        Thread.Sleep(500);

        var newUrl = ChromeDriver.Url;

        var urlParts = newUrl.Split("/");

        var result = int.TryParse(urlParts.Last(), out _);
        
        Assert.True(result);
    }
}