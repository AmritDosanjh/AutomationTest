﻿using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace AutomationTest
{
    [Binding]
    public class Steps
    {
        // Declaring the initial driver
        public IWebDriver driver;

        // Method to set up the driver with the root location and added chrome options.
        // Chrome driver set up as public above so that all other methods can access it.
        public IWebDriver SetUpDriver()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("start-maximized");
            return driver = new ChromeDriver(".", chromeOptions);
        }

        // Used driver to navigate to Galactico Eleven website.
        // Logged in to page.
        [Given(@"The user navigates to Galactico eleven")]
        public void GivenTheUserNavigatesToGalacticoEleven()
        {
            SetUpDriver();

            driver.Navigate().GoToUrl("https://www.galacticoeleven.com/#!/login");

            Thread.Sleep(5000);

            IWebElement loginInInput = driver.FindElement(By.XPath("//input[@type='email']"));
            IWebElement passwordInInput = driver.FindElement(By.XPath("//input[@type='password']"));
            IWebElement loginButton = driver.FindElement(By.XPath("//button[@ng-click='login()']"));

            loginInInput.SendKeys("heiiigssxltxobiujl@bvhrs.com");
            passwordInInput.SendKeys("Mortgagesupport1");
            loginButton.Click();
        }

        // Clicked on create leagues tab.
        [When(@"The user Clicks on the create leagues tabs")]
        public void WhenTheUserClicksOnTheCreateLeaguesTabs()
        {
            Thread.Sleep(5000);

            IWebElement createTab = driver.FindElement(By.XPath("(//a[contains(.,'Create')])[1]"));

            createTab.Click();

            Thread.Sleep(5000);
        }

        // Entered league name.
        // Entered team name.
        // Selected type of competition from dropdown.
        // Clicked create league.
        // Asserted created league details were correct.
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

            string firstBoxText = driver.FindElement(By.XPath("(//div[@class='well ng-scope'])[1]//h5")).Text;
            Assert.IsTrue(firstBoxText.Contains("Saiyans"), "It was expected for the first element text box to say Saiyans, but it did not");

            string createdCompeitionName = driver.FindElement(By.XPath("(//div[@class='well ng-scope'])[1]//h5//small[1]")).Text;
            Assert.IsTrue(createdCompeitionName.Contains("Competition: EPL 2021/22"), "It was expected for the competition name to be 'Competition: EPL 2021/22', but it did not");

            string createdTeamName = driver.FindElement(By.XPath("(//div[@class='well ng-scope'])[1]//h5//small[2]")).Text;
            Assert.IsTrue(createdTeamName.Contains("Team: Universe 7"), "It was expected for the team name to be 'Team: Universe 7', but it did not");

            driver.Close();
        }
    }
}
