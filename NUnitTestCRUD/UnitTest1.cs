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


namespace Tests
{
    public class UserPageObject
    {
        private string userNameInputFieldXpath;
        private string userAgeInputField;
        private string userEmailInputField;
        private string deleteLinkInputField;
        private string editLinkInputField;
        private TablePageObject tablePageObject; 


        private IWebDriver driver;

        public UserPageObject(IWebDriver driver)
        {
            this.driver = driver;
            //tablePageObject = driver.FindElements(By.TagName("tr"));
        }

        //public IWebElement UserNameInputField
        //{
        //    get
        //    {
        //        return driver.FindElement(By.CssSelector(MY_TASKS_CSS));
        //    }
        //}

    }


    public class TablePageObject
    {
        private IWebDriver driver;
        

        public TablePageObject(IWebDriver driver)
        {
            this.driver = driver;
            var tablePageObject = driver.FindElements(By.TagName("tr"));
        }


        //public List<UserPageObject> UserList
        //{
        //    get
        //    {
        //        //return driver.FindElement(By.CssSelector(DEPARTMENTS_CSS));
        //    }
        //}

    }

    public class Tests
    {
        //private const string REMOTE_BROWSER_NAME = "firefox";
        //private const string REMOTE_BROWSER_VERSION = "63.0";
        private const int BROWSER_WIDTH = 1920;
        private const int BROWSER_HEIGHT = 1080;
        private IWebDriver driver = null;
        private WebDriverWait wait = null;
        private const int IMPLICIT_WAIT_SECONDS = 15;
        private const int EXPLICIT_WAIT_SECONDS = 10;
        private const string APPLICATION_URL = "http://107.22.154.93/crud-php-simple/";
        private const string userName = "Ivan Ivanov";
        private const string userAge = "35";
        private const string userEmail = "ivan@gmail.com";       
        private IWebElement deleteUserLink1;

        [SetUp]
        public void Setup()
        {
            //driver = new FirefoxDriver();
            var universalDriverPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            ///home/viktor/NUnitTestCRUD/NUnitTestCRUD/bin/Debug/netcoreapp2.2           

            ChromeOptions options = new ChromeOptions();
            options.AddArgument("headless");
            options.AddArgument("--disable-gpu");
            options.AddArgument("disable-infobars");
            options.AddArgument("--disable-extensions");
            options.AddArguments("--no-sandbox");
            options.AddArguments("--disable-dev-shm-usage");
            // Must maximize Chrome by `start-maximized`
            options.AddArguments("start-maximized");

            // driver = new ChromeDriver("/home/viktor/NUnitTestCRUD/NUnitTestCRUD/Driver", options);
           driver = new ChromeDriver("/home/ubuntu/NUnitTestCRUD/NUnitTestCRUD/Driver", options);
            //driver = new ChromeDriver(universalDriverPath, options);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(EXPLICIT_WAIT_SECONDS));
        }

        [Test]
        public void Test1()
        {            
           
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(IMPLICIT_WAIT_SECONDS);
            //driver.Manage().Window.Size = new System.Drawing.Size(BROWSER_WIDTH, BROWSER_HEIGHT);
            driver.Navigate().GoToUrl("http://107.22.154.93/crud-php-simple/");

            Thread.Sleep(1000);

            var ls = driver.FindElement(By.XPath("/html/body/a"));
            ls.Click();

            //Thread.Sleep(1000);

            var name = driver.FindElement(By.XPath("/html/body/form/table/tbody/tr[1]/td[2]/input"));
            name.SendKeys(userName);

            //Thread.Sleep(1000);

            var age = driver.FindElement(By.XPath("/html/body/form/table/tbody/tr[2]/td[2]/input"));
            age.SendKeys(userAge);

           // Thread.Sleep(1000);

            var email = driver.FindElement(By.XPath("/html/body/form/table/tbody/tr[3]/td[2]/input"));
            email.SendKeys(userEmail);

           // Thread.Sleep(1000);

            var addButton = driver.FindElement(By.XPath("/html/body/form/table/tbody/tr[4]/td[2]/input"));
            addButton.Click();

            var viewresultLink = driver.FindElement(By.XPath("/html/body/font/a"));
            viewresultLink.Click();

            // document.querySelector("body > a")/html/body/form/table/tbody/tr[4]/td[2]/input
            /// /html/body/font/a
            Thread.Sleep(1000);
            var resultRows = driver.FindElements(By.TagName("tr"));

            var result = false;

           

            foreach (var row in resultRows)
            {
                var items = row.FindElements(By.TagName("td"));
                var resultName = items[0].Text;
                var resultAge = items[1].Text;
                var resultEmail = items[2].Text;

                if (items[0].Text == userName && items[1].Text==userAge && items[2].Text == userEmail)
                {
                    result = true;
                    deleteUserLink1 = items[3];                   
                    break;
                }               
            }


            ///html/body/table/tbody/tr[2]/td[4]/a[2]
            Assert.AreEqual(true,result);            
        }


        [TearDown]
        public void TearDown()
        {
            var deleteLink = deleteUserLink1.FindElement(By.XPath("//a[2]"));
            deleteLink.Click();


            //Thread.Sleep(1000);

            //wait.Until(ExpectedConditions.AlertIsPresent());
            driver.SwitchTo().Alert().Accept();
            //var alert = driver.SwitchTo().Alert();            
            //alert.Accept();

            //bool presentFlag = false;

            //try
            //{

            //    // Check the presence of alert
            //    var alert = driver.SwitchTo().Alert();
            //    // Alert present; set the flag
            //    //presentFlag = true;
            //    // if present consume the alert
            //    alert.Accept();

            //}
            //catch (NoAlertPresentException ex)
            //{
            //    // Alert not present
            //    //ex.printStackTrace();
            //}

            //return presentFlag;



            driver.Close();
            driver.Quit();
        }
    }
}
