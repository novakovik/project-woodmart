using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace FinalProjectSelenium
{
	public class CompareProducts
	{
		public IWebDriver driver;

		public CompareProducts(IWebDriver driver)
		{
			this.driver = driver;
            this.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }
        public void GoToClocks()
        {
            IWebElement clocks = driver.FindElement(By.Id("menu-item-433"));
            clocks.Click();
        }

        public void AddProductToCompare(int productId)
        {
            IWebElement productElement = driver.FindElement(By.CssSelector($"div[data-id=\"{productId}\"]"));
            Actions actionToPerform = new Actions(driver);
            actionToPerform.MoveToElement(productElement).Perform();

            IWebElement compare = driver.FindElement(By.CssSelector($"a[data-id=\"{productId}\"]"));
            compare.Click();
        }

        public void GoToComparePage()
        {
            IWebElement compareAll = driver.FindElement(By.CssSelector("a[title=\"Compare products\"]"));
            compareAll.Click();
        }
    }

}

