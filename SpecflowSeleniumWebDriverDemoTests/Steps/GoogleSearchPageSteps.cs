using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace SpecflowSeleniumWebdriverDemo
{
    [Binding]
    public class GoogleSearchPageSteps
    {
        // For additional details on SpecFlow step definitions see http://go.specflow.org/doc-stepdef

        [Given(@"I enter a search word (.*) into the Google search field")]
        public void GivenIEnterASearchWordCheeseIntoTheGoogleSearchField(string searchWord)
        {
            // Find the text input element by its name
            IWebElement query = ScenarioContext.Current.Get<IWebDriver>("IWebDriver").FindElement(By.XPath("//input[@name='q']"));

            // Enter something to search for
            query.SendKeys(searchWord);

            ScenarioContext.Current.Add("IWebElement", query);
            ScenarioContext.Current.Add("SearchWord", searchWord);
        }

        [When(@"I press the Search button")]
        public void WhenIPressTheSearchButton()
        {
            // Now submit the form. WebDriver will find the form for us from the element
            IWebElement searchButton = ScenarioContext.Current.Get<IWebDriver>("IWebDriver").FindElement(By.XPath("//button[@value='Search']"));
            searchButton.Click();
        }

        [Then(@"results related to search word should display")]
        public void ThenResultsRelatedToSearchWordShouldDisplay()
        {
            var driver = ScenarioContext.Current.Get<IWebDriver>("IWebDriver");

            // Google's search is rendered dynamically with JavaScript.
            // Wait for the page to load, timeout after 10 seconds
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until((d) => { return d.PageSource.Contains(ScenarioContext.Current.Get<string>("SearchWord")); });

            // Should see: "Cheese - Google Search"
            System.Console.WriteLine("Page title is: " + driver.Title);

            //Close the browser
            driver.Quit();
        }


        [Given(@"I enter a word ""(.*)"" in the search field")]
        public void GivenIEnterAWordInTheSearchField(string searchWord)
        {
            // Find the text input element by its name
            IWebElement query = ScenarioContext.Current.Get<IWebDriver>("IWebDriver").FindElement(By.XPath("//input[@name='q']"));

            // Enter something to search for
            query.SendKeys(searchWord);

            ScenarioContext.Current.Add("IWebElement", query);
            ScenarioContext.Current.Add("SearchWord", searchWord);
        }

        [Given(@"I go to the Google Home Page")]
        public void GivenIGoToTheGoogleHomePage()
        {
            IWebDriver driver = this.GetWebDriver(ConfigurationSettings.AppSettings["Browser"].ToString());

            //Notice navigation is slightly different than the Java version
            //This is because 'get' is a keyword in C#
            driver.Navigate().GoToUrl("http://www.google.com/");

            ScenarioContext.Current.Add("IWebDriver", driver);
        }

        public IWebDriver GetWebDriver(string browser)
        {
            IWebDriver driver = null;

            switch (browser)
            {
                case "FireFox":
                    driver = new FirefoxDriver();
                    break;
                case "IE":
                    driver = new InternetExplorerDriver();
                    break;
                case "Chrome":
                    driver = new ChromeDriver();
                    break;
                case "PhantonJS":
                    driver = new PhantomJSDriver();
                    break;
                default:
                    Console.WriteLine("No Driver");
                    break;
            }
            return driver;
        }
    }
}
