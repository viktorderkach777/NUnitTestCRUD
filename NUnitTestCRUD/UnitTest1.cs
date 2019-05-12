using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;

namespace Tests
{
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

        [SetUp]
        public void Setup()
        {

            IWebDriver driver = new ChromeDriver(".");//C:\Users\Віктор\Documents\Visual Studio 2017\Projects\NUnitTestCRUD\NUnitTestCRUD\UnitTest1.cs
            //driver = new FirefoxDriver();

        }

        [Test]
        public void Test1()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(IMPLICIT_WAIT_SECONDS);
            driver.Manage().Window.Size = new System.Drawing.Size(BROWSER_WIDTH, BROWSER_HEIGHT);
            driver.Navigate().GoToUrl("https//www.google.com");


            //Assert.Pass();
            Assert.AreEqual(1, 1);
        }
    }
}