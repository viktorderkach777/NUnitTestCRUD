using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium.Remote;
using System.Drawing;


namespace Tests
{

   static public class SelenoidDrivers
    {
        
        //private const bool ENABLE_VNC = true;
        private const int BROWSER_WIDTH = 1920;
        private const int BROWSER_HEIGHT = 1080;
        private const string URI = "http://35.232.250.225:4444/wd/hub";



        static public IWebDriver CreateDriver(string browser, string version)
        {
            ChromeOptions options = new ChromeOptions();
            //options.AddArgument("headless");
            //options.AddArgument("disable-gpu");


            options.AddArgument("test-type=browser");
            options.AddAdditionalCapability("enableVNC", true, true);          
            //options.AddAdditionalCapability(CapabilityType.Platform, "LINUX", true);
            options.AddAdditionalCapability(CapabilityType.BrowserName, browser, true);
            options.AddAdditionalCapability(CapabilityType.BrowserVersion, version, true);
            var driver = new RemoteWebDriver(new Uri(URI), options.ToCapabilities());

            //var capabilities = new DesiredCapabilities(browser, version, new Platform(PlatformType.Any));
            //capabilities.SetCapability("enableVNC", ENABLE_VNC);
            //var driver = new RemoteWebDriver(new Uri(URI), capabilities);


            driver.Manage().Window.Size = new Size(BROWSER_WIDTH, BROWSER_HEIGHT);
            return driver;
        }
    }



    public class Tests
    {        
        private IWebDriver driver = null;
        private WebDriverWait wait = null;
        private const int IMPLICIT_WAIT_SECONDS = 15;
        private const int EXPLICIT_WAIT_SECONDS = 10;
        private const string APPLICATION_URL = "http://35.196.233.219/crud-php-simple/";
        private const string userName = "Ivan Ivanov";
        private const string userAge = "35";
        private const string userEmail = "ivan@gmail.com";       
        private IWebElement deleteUserLink1;

        public const string CHROME = "chrome";
        public const string FIREFOX = "firefox";
        public const string CHROME_V1 = "73.0";
        public const string CHROME_V2 = "74.0";
        public const string FIREFOX_V1 = "66.0";
        public const string FIREFOX_V2 = "67.0";
       
        

        [SetUp]
        public void Setup()
        {            
            var universalDriverPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);                     

            ChromeOptions options = new ChromeOptions();
            options.AddArgument("headless");
            options.AddArgument("--disable-gpu");
            options.AddArgument("disable-infobars");
            options.AddArgument("--disable-extensions");
            options.AddArguments("--no-sandbox");
            options.AddArguments("--disable-dev-shm-usage");            
            options.AddArguments("start-maximized");

             //driver = new ChromeDriver("/home/viktor/NUnitTestCRUD/NUnitTestCRUD/Driver", options);             
           //driver = new ChromeDriver("/home/viktorderkach7777/NUnitTestCRUD/NUnitTestCRUD/Driver", options);
           //driver = new ChromeDriver(universalDriverPath);
           this.driver = SelenoidDrivers.CreateDriver(CHROME, CHROME_V2);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(EXPLICIT_WAIT_SECONDS));
        }

        [Test]
        public void Test1()
        {            
           
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(IMPLICIT_WAIT_SECONDS);           
            driver.Navigate().GoToUrl("http://35.196.233.219/crud-php-simple/");
          

            var ls = driver.FindElement(By.XPath("/html/body/a"));
            ls.Click();            

            var name = driver.FindElement(By.XPath("/html/body/form/table/tbody/tr[1]/td[2]/input"));
            name.SendKeys(userName);            

            var age = driver.FindElement(By.XPath("/html/body/form/table/tbody/tr[2]/td[2]/input"));
            age.SendKeys(userAge);          

            var email = driver.FindElement(By.XPath("/html/body/form/table/tbody/tr[3]/td[2]/input"));
            email.SendKeys(userEmail);           

            var addButton = driver.FindElement(By.XPath("/html/body/form/table/tbody/tr[4]/td[2]/input"));
            addButton.Click();

            var viewresultLink = driver.FindElement(By.XPath("/html/body/font/a"));
            viewresultLink.Click();
            
            
            var resultRows = driver.FindElements(By.TagName("tr"));

            var result = false;           

            foreach (var row in resultRows)
            {
                var items = row.FindElements(By.TagName("td"));              

                if (items[0].Text == userName && items[1].Text == userAge && items[2].Text == userEmail)
                {
                    result = true;
                    deleteUserLink1 = items[3];
                    break;
                }

                
            }
           
            Assert.AreEqual(result, true);            
        }


        [TearDown]
        public void TearDown()
        {           
           var deleteLink = deleteUserLink1.FindElement(By.XPath("//a[2]"));
          deleteLink.Click();
            
           driver.Quit();
        }
    }
}
