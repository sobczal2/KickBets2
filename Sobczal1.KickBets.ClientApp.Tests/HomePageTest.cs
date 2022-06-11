using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace Sobczal1.KickBets.ClientApp.Tests;

public class HomePageTest : IDisposable
{
    public HomePageTest()
    {
        ChromeDriver = new ChromeDriver();
    }

    public ChromeDriver ChromeDriver { get; set; }

    public void Dispose()
    {
        ChromeDriver.Dispose();
    }

    [Fact]
    public void Should_Redirect_OnGetStartedButtonClicked()
    {
        ChromeDriver.Navigate().GoToUrl("http://localhost:3000/");
        var button = ChromeDriver.FindElement(By.Id("homePageButton"));
        button.Click();

        var expectedUrl = "http://localhost:3000/fixtures";
        var actualUrl = ChromeDriver.Url;

        Assert.Equal(expectedUrl, actualUrl);
    }
}