using Newtonsoft.Json.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using RestSharp;
using TechTalk.SpecFlow;

namespace AutomationTest
{
    [Binding]
    public class Steps
    {
        // Declaring the initial driver.
        public IWebDriver driver;

        // Declare token to use for API Requests.
        public string AuthenticationToken;
        public string CreatedLeagueId;
        public string createdLeageName;

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

            // Navigate to URL.
            driver.Navigate().GoToUrl("https://www.galacticoeleven.com/#!/login");

            // Wait for page to load.
            Thread.Sleep(5000);

            // Finding of login elements.
            IWebElement loginInInput = driver.FindElement(By.XPath("//input[@type='email']"));
            IWebElement passwordInInput = driver.FindElement(By.XPath("//input[@type='password']"));
            IWebElement loginButton = driver.FindElement(By.XPath("//button[@ng-click='login()']"));

            //Sent login details. 
            // Clicked  login.
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

            // Selected down by text.
            leagueName.SendKeys("Saiyans");
            teamName.SendKeys("Universe 7");
            competitionNameDropdown.SelectByText("EPL 2021/22");
            createButton.Click();

            Thread.Sleep(5000);

            // Assert Error message added.
            string firstBoxText = driver.FindElement(By.XPath("(//div[@class='well ng-scope'])[1]//h5")).Text;
            Assert.IsTrue(firstBoxText.Contains("Saiyans"), "It was expected for the first element text box to say Saiyans, but it did not");

            string createdCompeitionName = driver.FindElement(By.XPath("(//div[@class='well ng-scope'])[1]//h5//small[1]")).Text;
            Assert.IsTrue(createdCompeitionName.Contains("Competition: EPL 2021/22"), "It was expected for the competition name to be 'Competition: EPL 2021/22', but it did not");

            string createdTeamName = driver.FindElement(By.XPath("(//div[@class='well ng-scope'])[1]//h5//small[2]")).Text;
            Assert.IsTrue(createdTeamName.Contains("Team: Universe 7"), "It was expected for the team name to be 'Team: Universe 7', but it did not");

            // Closed driver.
            driver.Close();
        }

        // Sends POST request to the login to obtain authentication token. 
        [Given(@"the user POSTS usernmae and password to GalacticoEleven login")]
        public void GivenTheUserPOSTSUsernmaeAndPasswordToGalacticoElevenLogin()
        {
            // RestClient sets the URL to which we are going to hit.
            var client = new RestClient("https://www.galacticoeleven.com/login");

            // Setting the request method type we want to sent, i.e. GET, POST, PUT etc.
            var request = new RestRequest(Method.POST);

            // Add JSON body to request.
            request.AddJsonBody("{\"email\":\"heiiigssxltxobiujl@bvhrs.com\",\"password\":\"Mortgagesupport1\"}");

            // Execute request to client(URL) with request(Method and JSON Body).
            IRestResponse response = client.Execute(request);

            // Stores token in string.
            AuthenticationToken = response.Content;

            // Write out the API response to the console.
            Console.WriteLine("This is the token: " + AuthenticationToken);
        }

        // Creates a new league using the POST leagues URL
        [When(@"the user POSTS a new league")]
        public void WhenTheUserPOSTSANewLeague()
        {
            // RestClient sets the URL to which we are going to hit.
            var client = new RestClient("https://www.galacticoeleven.com/api/league/");

            // Setting the request method type we want to sent, i.e. GET, POST, PUT etc.
            var request = new RestRequest(Method.POST);

            // Adding authentication token captured earlier.
            request.AddHeader("authorization", "Bearer " + AuthenticationToken);

            request.AddJsonBody("{\"name\":\"SaiyanAPI\",\"competition\":4}");

            // Execute request to client(URL) with request(Method and JSON Body).
            IRestResponse response = client.Execute(request);

            Console.WriteLine("Created League Output: " + response.Content);

            // Stores the API Response into a JSON object, so that we can capture individual elements of the JSON.
            var JSONResponse = JObject.Parse(response.Content);

            // Store JSON Object properties into strings.
            CreatedLeagueId = JSONResponse["_id"].ToString();
            createdLeageName = JSONResponse["name"].ToString();
        }

        // Sends a GET request to the leagues URL to return all leagues.
        // Assert newly created league exists in response.
        [Then(@"the user GETS the leagues to validate the new league has been created")]
        public void ThenTheUserGETSTheLeaguesToValidateTheNewLeagueHasBeenCreated()
        {
            // RestClient sets the URL to which we are going to hit.
            var client = new RestClient("https://www.galacticoeleven.com/api/league/");

            // Setting the request method type we want to sent, i.e. GET, POST, PUT etc.
            var request = new RestRequest(Method.GET);

            // Adding authentication token captured earlier.
            request.AddHeader("authorization", "Bearer " + AuthenticationToken);

            // Execute request to client(URL) with request(Method and JSON Body).
            IRestResponse response = client.Execute(request);

            Console.WriteLine("Returned League Output: " + response.Content);
        }


    }
}
