using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace AutomationTest
{
    [Binding]
    public class Steps
    {
        [Given(@"The user navigates to Galactico eleven")]
        public void GivenSomeGeezerGoesToGoogle()
        {
            IWebDriver driver = new ChromeDriver(".");
            driver.Navigate().GoToUrl("https://www.galacticoeleven.com/#!/login");

            Thread.Sleep(5000);

            IWebElement loginInInput = driver.FindElement(By.XPath("//input[@type='email']"));
            IWebElement passwordInInput = driver.FindElement(By.XPath("//input[@type='password']"));
            IWebElement loginButton = driver.FindElement(By.XPath("//button[@ng-click='login()']"));

            loginInInput.SendKeys("heiiigssxltxobiujl@bvhrs.com");
            passwordInInput.SendKeys("Mortgagesupport1");
            loginButton.Click();
        }
    }
}
