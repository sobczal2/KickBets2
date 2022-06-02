using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace Sobczal1.KickBets.ClientApp.Tests;

public class LoginPageTest
{
    [Fact]
    public void Should_Redirect_OnAboutButtonClicked()
    {
        var driver = new ChromeDriver();
        driver.Navigate().GoToUrl("http://localhost:3000/login/");
        var button = driver.FindElement(By.XPath("//*[text()='Register instead']"));
        button.Click();
        var expectedUrl = "http://localhost:3000/register";
        var actualUrl = driver.Url;
        
        driver.Dispose();
        
        Assert.Equal(expectedUrl, actualUrl);
    }

    [Fact]
    public void Should_Redirect_AfterSuccessfulLogin()
    {
        var driver = new ChromeDriver();
        driver.Navigate().GoToUrl("http://localhost:3000/login/");

        var emailField = driver.FindElement(By.Id("Email"));
        emailField.SendKeys("tester@test.com");
        
        var passwordField = driver.FindElement(By.Id("Password"));
        passwordField.SendKeys("password");
        
        var submitButton = driver.FindElement(By.Id("loginSubmitButton"));
        submitButton.Click();
        
        Thread.Sleep(5000);
        
        var expectedUrl = "http://localhost:3000/fixtures";
        var actualUrl = driver.Url;
        
        driver.Dispose();
        
        Assert.Equal(expectedUrl, actualUrl);
    }
}