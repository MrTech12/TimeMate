using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using TimeMate.Controllers;
using TimeMate.Models;
using Xunit;

namespace TimeMateTest.Presentation_Layer
{
    public class AccountControllerTest: IDisposable
    {
        private readonly IWebDriver _driver;

        AccountController accountController;

        public AccountControllerTest()
        {
            _driver = new ChromeDriver();
        }


        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        [Fact]
        public void LoggingInWithNoPassword()
        {
            
        }
    }
}
