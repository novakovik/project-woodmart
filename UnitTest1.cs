namespace FinalProjectSelenium;

using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;




public class Tests
{
    public IWebDriver driver;
    public MyAccount myAccount;
    public CompareProducts compareProducts;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        driver = new ChromeDriver();

        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
        driver.Manage().Window.Maximize();

        myAccount = new MyAccount(driver);
        compareProducts = new CompareProducts(driver);
    }

    [Test]
    [Order(1)]
    public void Register()
    {
        driver.Navigate().GoToUrl("https://woodmart.xtemos.com/home/");

        myAccount.CreateAccountPage();

        string suffix = DateTime.Now.ToString().Replace('/', '1').Replace(':', '2').Replace(" ", "3");
        string email = "ivananovakovik8+" + suffix + "@gmail.com";
        myAccount.RegisterAccount("ivana-" + suffix, email, "IvanaIvana12!I");

        myAccount.GoToAccountDetails();

        IWebElement accountEmailValidation = driver.FindElement(By.Id("account_email"));
        Assert.That(accountEmailValidation.GetAttribute("value"), Is.EqualTo(email));
    }

    [Test]
    [Order(2)]
    public void CompareProducts()
    {
        driver.Navigate().GoToUrl("https://woodmart.xtemos.com/home/");

        compareProducts.GoToClocks();

        compareProducts.AddProductToCompare(1162);
        compareProducts.AddProductToCompare(1055);
        compareProducts.GoToComparePage();

        IReadOnlyCollection<IWebElement> compareValues = driver.FindElements(By.CssSelector("tr.compare-basic > .compare-value > a.wd-entities-title"));

        Assert.That(compareValues.First().Text, Is.EqualTo("Best clock parallels"));
        Assert.That(compareValues.Last().Text, Is.EqualTo("Dolor ad hac torquent"));
    }

    [Test]
    [Order(3)]
    public void CheckDiscountedPrice()
    {
        IWebElement discountFirstElement = driver.FindElement(By.CssSelector("tr.compare-basic > td.compare-value:first-of-type > div.price > del"));
        Assert.IsNotNull(discountFirstElement);
    }

    [Test]
    [Order(4)]
    public void CheckRating()
    {
        IWebElement ratingFistElement = driver.FindElement(By.CssSelector("tr.compare-basic > td.compare-value:first-of-type > div.star-rating"));
        Console.WriteLine(ratingFistElement.GetAttribute("aria-label"));

        Assert.That(ratingFistElement.GetAttribute("aria-label"), Is.EqualTo("Rated 5.00 out of 5"));
    }

    [Test]
    [Order(5)]
    public void CheckAvailability()
    {
        IReadOnlyCollection<IWebElement> inStockValues = driver.FindElements(By.CssSelector("tr.compare-availability > td.compare-value p.stock"));

        foreach (IWebElement element in inStockValues)
        {
            Assert.That(element.Text, Is.EqualTo("In stock"));
        }
    }

    [Test]
    [Order(6)]
    public void ConfirmDifferentBrands()
    {
        IWebElement brand1 = driver.FindElement(By.CssSelector("tr.compare-pa_brand > td.compare-value:first-of-type > .wd-compare-brand > picture"));
        IWebElement brand2 = driver.FindElement(By.CssSelector("tr.compare-pa_brand > td.compare-value:last-of-type > .wd-compare-brand > picture"));

        Assert.That(brand2.GetAttribute("title"), Is.Not.EqualTo(brand1.GetAttribute("title")));
    }

    [Test]
    [Order(7)]
    public void EmptyColorSelection()
    {
        IWebElement color = driver.FindElement(By.CssSelector("tr.compare-pa_color > td.compare-value:last-of-type"));
        Assert.That(color.Text, Is.EqualTo("-"));
    }

    [Test]
    [Order(8)]
    public void Bonus()
    {
        IWebElement removeElement1 = driver.FindElement(By.CssSelector("a.wd-compare-remove"));
        removeElement1.Click();

        IReadOnlyCollection<IWebElement> removableElements = driver.FindElements(By.CssSelector("a.wd-compare-remove"));
        while (removableElements.Count() >= 2)
        {
            //Refresh the removable elements list until there are less than two elements so that the last element can be removed.
            removableElements = driver.FindElements(By.CssSelector("a.wd-compare-remove"));
        }

        IWebElement removeElement2 = driver.FindElement(By.CssSelector("a.wd-compare-remove"));
        removeElement2.Click();

        Thread.Sleep(2000);
        Actions escapePopUp = new Actions(driver);
        escapePopUp.SendKeys(Keys.Escape).Perform();

        IWebElement compareMessage = driver.FindElement(By.CssSelector("p.wd-empty-compare.wd-empty-page"));
        Assert.That(compareMessage.Text, Is.EqualTo("Compare list is empty."));

        IWebElement compareNumber = driver.FindElement(By.CssSelector("a[title=\"Compare products\"] > span > span.wd-tools-count "));
        Assert.That(Convert.ToInt32(compareNumber.Text), Is.EqualTo(0));
    }


    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        myAccount.Logout();
        driver.Quit();
    }
}
