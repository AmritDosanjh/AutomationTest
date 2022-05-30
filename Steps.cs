using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace AutomationTest
{
    [Binding]
    public class Steps
    {
        public IWebDriver driver = new ChromeDriver(".");

        [Given(@"The user navigates to Galactico eleven")]
        public void GivenSomeGeezerGoesToGoogle()
        {
            driver.Navigate().GoToUrl("https://www.galacticoeleven.com/#!/login");

            Thread.Sleep(5000);

            IWebElement loginInInput = driver.FindElement(By.XPath("//input[@type='email']"));
            IWebElement passwordInInput = driver.FindElement(By.XPath("//input[@type='password']"));
            IWebElement loginButton = driver.FindElement(By.XPath("//button[@ng-click='login()']"));

            loginInInput.SendKeys("heiiigssxltxobiujl@bvhrs.com");
            passwordInInput.SendKeys("Mortgagesupport1");
            loginButton.Click();
        }

        [When(@"The user Clicks on the create leagues tabs")]
        public void WhenTheUserClicksOnTheCreateLeaguesTabs()
        {
            Thread.Sleep(5000);

            IWebElement createTab = driver.FindElement(By.XPath("(//a[contains(.,'Create')])[1]"));

            createTab.Click();

            Thread.Sleep(5000);
        }

        [Then(@"The users creates a league")]
        public void ThenTheUsersCreatesALeague()
        {
            IWebElement leagueName = driver.FindElement(By.Id("name"));
            IWebElement teamName = driver.FindElement(By.Id("team"));
            IWebElement competitionName = driver.FindElement(By.Id("competition"));
            SelectElement competitionNameDropdown = new SelectElement(competitionName);
            IWebElement createButton = driver.FindElement(By.XPath("//a[@ng-click='create()']"));

            leagueName.SendKeys("Saiyans");
            teamName.SendKeys("Universe 7");
            competitionNameDropdown.SelectByText("EPL 2021/22");
            createButton.Click();

            Thread.Sleep(5000);
        }

    }
}
