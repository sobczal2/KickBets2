using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace Sobczal1.KickBets.ClientApp.Tests;

public class NavBarTest : IDisposable
{
    public NavBarTest()
    {
        ChromeDriver = new ChromeDriver();
    }

    public ChromeDriver ChromeDriver { get; set; }

    public void Dispose()
    {
        ChromeDriver.Dispose();
    }

    [Fact]
    public void Should_Redirect_OnLoginButtonClicked()
    {
        ChromeDriver.Navigate().GoToUrl("http://localhost:3000/fixtures/");
        var button = ChromeDriver.FindElement(By.Id("navbarLoginButton"));
        button.Click();
        var expectedUrl = "http://localhost:3000/login";
        var actualUrl = ChromeDriver.Url;

        Assert.Equal(expectedUrl, actualUrl);
    }

    [Fact]
    public void Should_Redirect_OnRegisterButtonClicked()
    {
        ChromeDriver.Navigate().GoToUrl("http://localhost:3000/fixtures/");
        var button = ChromeDriver.FindElement(By.Id("navbarRegisterButton"));
        button.Click();
        var expectedUrl = "http://localhost:3000/register";
        var actualUrl = ChromeDriver.Url;

        Assert.Equal(expectedUrl, actualUrl);
    }

    [Fact]
    public void Should_Redirect_OnDashboardButtonClicked()
    {
        ChromeDriver.Navigate().GoToUrl("http://localhost:3000/about/");
        var button = ChromeDriver.FindElement(By.XPath("//*[text()='Dashboard']"));
        button.Click();

        var expectedUrl = "http://localhost:3000/fixtures";
        var actualUrl = ChromeDriver.Url;

        Assert.Equal(expectedUrl, actualUrl);
    }

    [Fact]
    public void Should_Redirect_OnMyBetsButtonClicked()
    {
        ChromeDriver.Navigate().GoToUrl("http://localhost:3000/fixtures/");
        var button = ChromeDriver.FindElement(By.XPath("//*[text()='My Bets']"));
        button.Click();
        var expectedUrl = "http://localhost:3000/mybets";
        var actualUrl = ChromeDriver.Url;

        Assert.Equal(expectedUrl, actualUrl);
    }

    [Fact]
    public void Should_Redirect_OnAboutButtonClicked()
    {
        ChromeDriver.Navigate().GoToUrl("http://localhost:3000/fixtures/");
        var button = ChromeDriver.FindElement(By.XPath("//*[text()='About']"));
        button.Click();
        var expectedUrl = "http://localhost:3000/about";
        var actualUrl = ChromeDriver.Url;

        Assert.Equal(expectedUrl, actualUrl);
    }

    [Fact]
    public void Should_Redirect_OnKickbetsButtonClicked()
    {
        ChromeDriver.Navigate().GoToUrl("http://localhost:3000/fixtures/");
        var button = ChromeDriver.FindElement(By.XPath("//*[text()='KICKBETS']"));
        button.Click();
        var expectedUrl = "http://localhost:3000/";
        var actualUrl = ChromeDriver.Url;

        Assert.Equal(expectedUrl, actualUrl);
    }

    [Fact]
    public void Should_ShowToast_OnAddBalance()
    {
        TestUtils.Login(ChromeDriver);
        
        ChromeDriver.Navigate().GoToUrl("http://localhost:3000/fixtures/");
        
        Thread.Sleep(500);

        var button = ChromeDriver.FindElement(By.Id("navbar-add-balance-button"));
        button.Click();
        
        Thread.Sleep(500);

        var toast = ChromeDriver.FindElement(By.ClassName("Toastify__toast-container"));
        
        Assert.NotNull(toast);
    }

    [Fact]
    public void Should_Logout_OnLogoutButtonClicked()
    {
        TestUtils.Login(ChromeDriver);
        
        Thread.Sleep(3000);
        
        var button = ChromeDriver.FindElement(By.Id("navbar-logout-button"));
        button.Click();
        
        Thread.Sleep(500);

        Assert.Throws<NoSuchElementException>(() =>
        {
            ChromeDriver.FindElement(By.Id("navbar-logout-button"));
        });
    }
}