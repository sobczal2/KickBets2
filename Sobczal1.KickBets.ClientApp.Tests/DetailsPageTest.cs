using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace Sobczal1.KickBets.ClientApp.Tests;

public class DetailsPageTest : IDisposable
{
    public DetailsPageTest()
    {
        ChromeDriver = new ChromeDriver();
    }

    public ChromeDriver ChromeDriver { get; set; }

    [Fact]
    public void Should_RenderStatisticsTab_WhenSelected()
    {
        ChromeDriver.Navigate().GoToUrl("http://localhost:3000/fixtures");

        Thread.Sleep(500);

        var items = ChromeDriver.FindElements(By.ClassName("dashboard-list-item-box"));
        
        Assert.True(items.Count >= 1);
        
        items[0].Click();
        Thread.Sleep(500);

        var statisticsTab = ChromeDriver.FindElement(By.Id("details-statistics-tab"));
        
        Assert.NotNull(statisticsTab);
    }
    
    [Fact]
    public void Should_RenderLineupsTab_WhenSelected()
    {
        ChromeDriver.Navigate().GoToUrl("http://localhost:3000/fixtures");

        Thread.Sleep(500);

        var items = ChromeDriver.FindElements(By.ClassName("dashboard-list-item-box"));
        
        Assert.True(items.Count >= 1);
        
        items[0].Click();
        Thread.Sleep(500);

        var lineupsTabButton = ChromeDriver.FindElement(By.Id("details-lineups-tab-selector"));
        lineupsTabButton.Click();
        
        Thread.Sleep(100);

        var lineupsTab = ChromeDriver.FindElement(By.Id("details-lineups-tab"));
        
        Assert.NotNull(lineupsTab);
    }
    
    [Fact]
    public void Should_RenderFormationTab_WhenSelected()
    {
        ChromeDriver.Navigate().GoToUrl("http://localhost:3000/fixtures");

        Thread.Sleep(500);

        var items = ChromeDriver.FindElements(By.ClassName("dashboard-list-item-box"));
        
        Assert.True(items.Count >= 1);
        
        items[0].Click();
        Thread.Sleep(500);

        var formationTabButton = ChromeDriver.FindElement(By.Id("details-formation-tab-selector"));
        formationTabButton.Click();
        
        Thread.Sleep(100);

        var formationTab = ChromeDriver.FindElement(By.Id("details-formation-tab"));
        
        Assert.NotNull(formationTab);
    }

    [Fact]
    public void Should_OpenModalForWdlhtBet_WhenPanelClicked()
    {
        ChromeDriver.Navigate().GoToUrl("http://localhost:3000/fixtures");

        Thread.Sleep(500);

        var items = ChromeDriver.FindElements(By.ClassName("dashboard-list-item-box"));
        
        Assert.True(items.Count >= 1);
        
        items[0].Click();
        Thread.Sleep(500);

        var panel = ChromeDriver.FindElement(By.Id("wdlht-panel"));
        panel.Click();

        Thread.Sleep(500);
        
        var button = ChromeDriver.FindElement(By.Id("wdlht-modal-submit-button"));
        
        Assert.NotNull(button);
    }
    
    [Fact]
    public void Should_OpenModalForWdlftBet_WhenPanelClicked()
    {
        ChromeDriver.Navigate().GoToUrl("http://localhost:3000/fixtures");

        Thread.Sleep(500);

        var items = ChromeDriver.FindElements(By.ClassName("dashboard-list-item-box"));
        
        Assert.True(items.Count >= 1);
        
        items[0].Click();
        Thread.Sleep(500);

        var panel = ChromeDriver.FindElement(By.Id("wdlft-panel"));
        panel.Click();

        Thread.Sleep(500);
        
        var button = ChromeDriver.FindElement(By.Id("wdlft-modal-submit-button"));
        
        Assert.NotNull(button);
    }
    
    [Fact]
    public void Should_PlaceWdlhtBet_WhenDoneCorrectly()
    {
        TestUtils.Login(ChromeDriver);
        
        Thread.Sleep(500);
        
        var beforeBetCount = TestUtils.CountBets(ChromeDriver, "wdlht");
        
        ChromeDriver.Navigate().GoToUrl("http://localhost:3000/fixtures");

        Thread.Sleep(500);

        var items = ChromeDriver.FindElements(By.ClassName("dashboard-list-item-box"));
        
        Assert.True(items.Count >= 1);
        
        items[0].Click();
        Thread.Sleep(500);

        var panel = ChromeDriver.FindElement(By.Id("wdlht-panel"));
        panel.Click();

        Thread.Sleep(500);
        
        var textField = ChromeDriver.FindElement(By.Id("wdlht-input-field"));
        
        textField.SendKeys(Keys.Backspace + Keys.Backspace + "1");
        
        Thread.Sleep(500);
        
        var button = ChromeDriver.FindElement(By.Id("wdlht-modal-submit-button"));
        
        button.Click();
        
        Thread.Sleep(100);
        
        TestUtils.Login(ChromeDriver);
        var afterBetCount = TestUtils.CountBets(ChromeDriver, "wdlht");
        
        Assert.NotEqual(beforeBetCount, afterBetCount);
    }
    
    [Fact]
    public void Should_PlaceWdlftBet_WhenDoneCorrectly()
    {
        TestUtils.Login(ChromeDriver);
        
        Thread.Sleep(500);
        
        var beforeBetCount = TestUtils.CountBets(ChromeDriver, "wdlft");
        
        ChromeDriver.Navigate().GoToUrl("http://localhost:3000/fixtures");

        Thread.Sleep(500);

        var items = ChromeDriver.FindElements(By.ClassName("dashboard-list-item-box"));
        
        Assert.True(items.Count >= 1);
        
        items[0].Click();
        Thread.Sleep(500);

        var panel = ChromeDriver.FindElement(By.Id("wdlft-panel"));
        panel.Click();

        Thread.Sleep(500);
        
        var textField = ChromeDriver.FindElement(By.Id("wdlft-input-field"));
        
        textField.SendKeys(Keys.Backspace + Keys.Backspace + "1");
        
        Thread.Sleep(500);
        
        var button = ChromeDriver.FindElement(By.Id("wdlft-modal-submit-button"));
        
        button.Click();
        
        Thread.Sleep(100);
        
        TestUtils.Login(ChromeDriver);
        var afterBetCount = TestUtils.CountBets(ChromeDriver, "wdlft");
        
        Assert.NotEqual(beforeBetCount, afterBetCount);
    }
    

    public void Dispose()
    {
        ChromeDriver.Dispose();
    }
}