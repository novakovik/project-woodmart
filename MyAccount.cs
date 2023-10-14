using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace FinalProjectSelenium
{
    public class MyAccount
    {
        public IWebDriver driver;

        public MyAccount(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void CreateAccountPage()
        {
            IWebElement register = driver.FindElement(By.CssSelector("a[title=\"My account\"]"));
            register.Click();

            IWebElement createaccount = driver.FindElement(By.CssSelector("a.create-account-button"));
            createaccount.Click();
        }

        public void RegisterAccount(string username, string email, string password)
        {
            IWebElement accountUsername = driver.FindElement(By.Id("reg_username"));
            accountUsername.SendKeys(username);

            IWebElement accountEmail = driver.FindElement(By.Id("reg_email"));
            accountEmail.SendKeys(email);

            IWebElement accountPassword = driver.FindElement(By.Id("reg_password"));
            accountPassword.SendKeys(password);

            IWebElement registerButton = driver.FindElement(By.CssSelector("button[value=\"Register\"]"));
            registerButton.Click();
        }

        public void GoToAccountDetails()
        {
            IWebElement accountDetails = driver.FindElement(By.LinkText("Account details"));
            accountDetails.Click();
        }

        public void Logout()
        {
            IWebElement myAccountMenu = driver.FindElement(By.CssSelector("div.wd-header-my-account.wd-tools-element.wd-event-hover.wd-design-1.wd-account-style-text"));
            Actions actionToPerform = new Actions(driver);
            actionToPerform.MoveToElement(myAccountMenu).Perform();

            IWebElement logout = driver.FindElement(By.CssSelector("div.wd-dropdown-my-account > ul > li.woocommerce-MyAccount-navigation-link--customer-logout"));
            logout.Click();
        }
    }
}

