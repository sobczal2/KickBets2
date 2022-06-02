using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace Sobczal1.KickBets.ClientApp.Tests;

public class NavBarTest
{
    [Fact]
    public void Should_Redirect_OnLoginButtonClicked()
    {
        var driver = new ChromeDriver();
        driver.Navigate().GoToUrl("http://localhost:3000/fixtures/");
        var button = driver.FindElement(By.Id("navbarLoginButton"));
        button.Click();
        var expectedUrl = "http://localhost:3000/login";
        var actualUrl = driver.Url;
        
        driver.Dispose();

        Assert.Equal(expectedUrl, actualUrl);
    }
    
    [Fact]
    public void Should_Redirect_OnRegisterButtonClicked()
    {
        var driver = new ChromeDriver();
        driver.Navigate().GoToUrl("http://localhost:3000/fixtures/");
        var button = driver.FindElement(By.Id("navbarRegisterButton"));
        button.Click();
        var expectedUrl = "http://localhost:3000/register";
        var actualUrl = driver.Url;
        
        driver.Dispose();
        
        Assert.Equal(expectedUrl, actualUrl);
    }
    
    [Fact]
    public void Should_Redirect_OnDashboardButtonClicked()
    {
        var driver = new ChromeDriver();
        driver.Navigate().GoToUrl("http://localhost:3000/about/");
        var button = driver.FindElement(By.XPath("//*[text()='Dashboard']"));
        button.Click();
        
        var expectedUrl = "http://localhost:3000/fixtures";
        var actualUrl = driver.Url;
        
        driver.Dispose();
        
        Assert.Equal(expectedUrl, actualUrl);
    }
    
    [Fact]
    public void Should_Redirect_OnMyBetsButtonClicked()
    {
        var driver = new ChromeDriver();
        driver.Navigate().GoToUrl("http://localhost:3000/fixtures/");
        var button = driver.FindElement(By.XPath("//*[text()='My Bets']"));
        button.Click();
        var expectedUrl = "http://localhost:3000/mybets";
        var actualUrl = driver.Url;
        
        driver.Dispose();
        
        Assert.Equal(expectedUrl, actualUrl);
    }
    
    [Fact]
    public void Should_Redirect_OnAboutButtonClicked()
    {
        var driver = new ChromeDriver();
        driver.Navigate().GoToUrl("http://localhost:3000/fixtures/");
        var button = driver.FindElement(By.XPath("//*[text()='About']"));
        button.Click();
        var expectedUrl = "http://localhost:3000/about";
        var actualUrl = driver.Url;
        
        driver.Dispose();
        
        Assert.Equal(expectedUrl, actualUrl);
    }
    
    [Fact]
    public void Should_Redirect_OnKickbetsButtonClicked()
    {
        var driver = new ChromeDriver();
        driver.Navigate().GoToUrl("http://localhost:3000/fixtures/");
        var button = driver.FindElement(By.XPath("//*[text()='KICKBETS']"));
        button.Click();
        var expectedUrl = "http://localhost:3000/";
        var actualUrl = driver.Url;
        
        driver.Dispose();
        
        Assert.Equal(expectedUrl, actualUrl);
    }
}