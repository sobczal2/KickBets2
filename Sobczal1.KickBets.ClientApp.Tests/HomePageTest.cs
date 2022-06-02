using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace Sobczal1.KickBets.ClientApp.Tests;

public class HomePageTest
{
    [Fact]
    public void Should_Redirect_OnGetStartedButtonClicked()
    {
        var driver = new ChromeDriver();
        driver.Navigate().GoToUrl("http://localhost:3000/");
        var button = driver.FindElement(By.Id("homePageButton"));
        button.Click();

        var expectedUrl = "http://localhost:3000/fixtures";
        var actualUrl = driver.Url;
        
        driver.Dispose();
        
        Assert.Equal(expectedUrl, actualUrl);
    }
}