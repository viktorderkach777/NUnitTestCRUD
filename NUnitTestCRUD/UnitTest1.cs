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

    public class TablePageObject
    {
        private const string TASK_LINK_TAGNAME = "tr";
        private IWebDriver driver;        

        public TablePageObject(IWebDriver driver)
        {
            this.driver = driver;           
        }       

        public List<UserPageObject> UserList
        {
            get
            {
                var result = new List<UserPageObject>();
                var tasks = driver.FindElements(By.TagName(TASK_LINK_TAGNAME));

                foreach (var task in tasks)
                {
                    result.Add(new UserPageObject(task));
                }

                return result;
            }
        }
    }


    public class UserPageObject
    {
        IWebElement parent;       

        private const string USER_NAME_XPATH = "//td[1]";
        private const string USER_AGE_XPATH = "//td[2]";
        private const string USER_EMAIL_XPATH = "//td[3]";
        private const string USER_EDIT_LINK_XPATH = "//td[4]/a[1]";
        private const string USER_DELETE_LINK_XPATH = "//td[4]/a[2]";

        public UserPageObject(IWebElement parent)
        {
            this.parent = parent;
        }

        public IWebElement UserName
        {
            get => parent.FindElement(By.XPath(USER_NAME_XPATH));
        }

        public IWebElement UserAge
        {
            get => parent.FindElement(By.XPath(USER_AGE_XPATH));
        }

        public IWebElement UserEmail
        {
            get => parent.FindElement(By.XPath(USER_EMAIL_XPATH));
        }

        public IWebElement UserEditLink
        {
            get => parent.FindElement(By.XPath(USER_EDIT_LINK_XPATH));
        }

        public IWebElement UserDeleteLink
        {
            get => parent.FindElement(By.XPath(USER_DELETE_LINK_XPATH));
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
        private UserPageObject user;
        

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
           driver = new ChromeDriver("/home/viktorderkach7777/NUnitTestCRUD/NUnitTestCRUD/Driver", options);
           //driver = new ChromeDriver(universalDriverPath);
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

                UserPageObject user = new UserPageObject(row);
               
                string s2 = user.UserName.Text;

                var resultName = items[0].Text;
                var resultAge = items[1].Text;
                var resultEmail = items[2].Text;

                if (items[0].Text == userName && items[1].Text == userAge && items[2].Text == userEmail)
                {
                    result = true;
                    deleteUserLink1 = items[3];
                    break;
                }

                //if (user.UserName.Text == userName && user.UserAge.Text == userAge && user.UserEmail.Text == userEmail)
                //{
                //    result = true;
                //    deleteUserLink1 = items[3];
                //    break;
                //}
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
