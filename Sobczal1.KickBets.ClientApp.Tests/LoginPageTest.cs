using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace Sobczal1.KickBets.ClientApp.Tests;

public class LoginPageTest : IDisposable
{
    public LoginPageTest()
    {
        ChromeDriver = new ChromeDriver();
    }

    public ChromeDriver ChromeDriver { get; set; }

    public void Dispose()
    {
        ChromeDriver.Dispose();
    }

    [Fact]
    public void Should_Redirect_OnAboutButtonClicked()
    {
        ChromeDriver.Navigate().GoToUrl("http://localhost:3000/login/");
        var button = ChromeDriver.FindElement(By.XPath("//*[text()='Register instead']"));
        button.Click();
        var expectedUrl = "http://localhost:3000/register";
        var actualUrl = ChromeDriver.Url;

        Assert.Equal(expectedUrl, actualUrl);
    }

    [Fact]
    public void Should_Redirect_AfterSuccessfulLogin()
    {
        ChromeDriver.Navigate().GoToUrl("http://localhost:3000/login/");
        
        TestUtils.Login(ChromeDriver);

        
        var expectedUrl = "http://localhost:3000/fixtures";
        var actualUrl = ChromeDriver.Url;

        Assert.Equal(expectedUrl, actualUrl);
    }
}