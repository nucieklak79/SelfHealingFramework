using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace SelfHealing.Core
{
    public class SelfHealingDriver
    {
        private readonly IWebDriver _driver;
        private readonly Healer _healer;

        public SelfHealingDriver(IWebDriver driver, Healer healer)
        {
            _driver = driver;
            _healer = healer;
        }

        public IWebElement FindElement(By locator, string description)
        {
            try
            {
                return _driver.FindElement(locator);
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine($"[Self-Healing] Element not found using: {locator}");
                Console.WriteLine($"[Self-Healing] Querying LLM to repair locator for: '{description}'...");
                
                string newLocatorString = _healer.HealLocator(locator.ToString(), description, _driver.PageSource);
                By newLocator = By.XPath(newLocatorString);
                
                Console.WriteLine($"[Self-Healing] LLM suggested new locator: {newLocator}");
                
                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
                wait.Until(d => d.FindElement(newLocator).Displayed);
                
                return _driver.FindElement(newLocator);
            }
        }
    }
}