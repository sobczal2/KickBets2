using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace Sobczal1.KickBets.ClientApp.Tests;

public class RegisterPageTest : IDisposable
{
    public RegisterPageTest()
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
        ChromeDriver.Navigate().GoToUrl("http://localhost:3000/register/");
        var button = ChromeDriver.FindElement(By.XPath("//*[text()='Login instead']"));
        button.Click();
        var expectedUrl = "http://localhost:3000/login";
        var actualUrl = ChromeDriver.Url;


        Assert.Equal(expectedUrl, actualUrl);
    }
}