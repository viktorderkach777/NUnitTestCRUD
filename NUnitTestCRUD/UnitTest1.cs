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
        private IWebDriver driver = null;
        private WebDriverWait wait = null;
        private const int IMPLICIT_WAIT_SECONDS = 15;
        private const int EXPLICIT_WAIT_SECONDS = 10;
        private const string APPLICATION_URL = "http://54.145.204.186/crud-php-simple/";
        private const string userName = "Ivan Ivanov";
        private const string userAge = "35";
        private const string userEmail = "ivan@gmail.com";       
        private IWebElement deleteUserLink1;

        [SetUp]
        public void Setup()
        {
            //driver = new FirefoxDriver();
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
           driver = new ChromeDriver("/home/ubuntu/NUnitTestCRUD/NUnitTestCRUD/Driver", options);
            //driver = new ChromeDriver(universalDriverPath);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(EXPLICIT_WAIT_SECONDS));
        }

        [Test]
        public void Test1()
        {            
           
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(IMPLICIT_WAIT_SECONDS);           
            driver.Navigate().GoToUrl("http://54.145.204.186/crud-php-simple/");

            //Thread.Sleep(1000);

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
            
            //Thread.Sleep(1000);
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
           
            Assert.AreEqual(result, true);            
        }


        [TearDown]
        public void TearDown()
        {
           //Thread.Sleep(1000);
           var deleteLink = deleteUserLink1.FindElement(By.XPath("//a[2]"));
          deleteLink.Click();
            
           driver.Quit();
        }
    }
}
