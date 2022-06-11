using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Sobczal1.KickBets.ClientApp.Tests;

public static class TestUtils
{
    public static void Login(ChromeDriver driver)
    {
        driver.Navigate().GoToUrl("http://localhost:3000/login/");

        var emailField = driver.FindElement(By.Id("Email"));
        emailField.SendKeys("tester@test.com");

        var passwordField = driver.FindElement(By.Id("Password"));
        passwordField.SendKeys("password");

        var submitButton = driver.FindElement(By.Id("loginSubmitButton"));
        submitButton.Click();

        Thread.Sleep(3000);
    }

    public static int CountBets(ChromeDriver driver, string betString)
    {
        driver.Navigate().GoToUrl("http://localhost:3000/mybets/");

        Thread.Sleep(500);
        
        var items = driver.FindElements(By.ClassName(betString + "-bet-item"));

        return items.Count;
    }
}