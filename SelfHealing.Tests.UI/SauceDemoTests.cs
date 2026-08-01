using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SelfHealing.Core;
using SelfHealing.Core.LLM;
using System;

namespace SelfHealing.Tests.UI
{
    [TestFixture]
    public class SauceDemoTests
    {
        private IWebDriver _driver;
        private SelfHealingDriver _healingDriver;

        [SetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddArgument("--headless=new");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");
            
            _driver = new ChromeDriver(options);
            
            var mockLlm = new MockLlmClient();
            _healingDriver = new SelfHealingDriver(_driver, new Healer(mockLlm));
        }

        [Test]
        public void Login_WithBrokenUsernameLocator_ShouldSelfHealAndSucceed()
        {
            // Arrange
            _driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            
            By brokenUsernameLocator = By.Id("user-name-changed-in-latest-release");

            // Act
            IWebElement usernameField = _healingDriver.FindElement(brokenUsernameLocator, "Username input field");
            usernameField.SendKeys("standard_user");
            
            _driver.FindElement(By.Id("password")).SendKeys("secret_sauce");
            _driver.FindElement(By.Id("login-button")).Click();

            // Assert
            Assert.That(_driver.Url, Does.Contain("inventory.html"));
        }

        [TearDown]
        public void Teardown()
        {
            _driver?.Quit();
            _driver?.Dispose();
        }
    }

    public class MockLlmClient : ILlmClient
    {
        public string SuggestNewLocator(string brokenLocator, string targetElementDescription, string pageSourceSnippet)
        {
            Console.WriteLine($"[Mock LLM] Analyzing DOM for '{targetElementDescription}'...");
            return "//*[@id='user-name']";
        }
    }
}