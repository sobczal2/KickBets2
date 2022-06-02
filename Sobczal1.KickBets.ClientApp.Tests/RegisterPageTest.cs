using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace Sobczal1.KickBets.ClientApp.Tests;

public class RegisterPageTest
{
    [Fact]
    public void Should_Redirect_OnAboutButtonClicked()
    {
        var driver = new ChromeDriver();
        driver.Navigate().GoToUrl("http://localhost:3000/register/");
        var button = driver.FindElement(By.XPath("//*[text()='Login instead']"));
        button.Click();
        var expectedUrl = "http://localhost:3000/login";
        var actualUrl = driver.Url;
        
        driver.Dispose();
        
        Assert.Equal(expectedUrl, actualUrl);
    }
}