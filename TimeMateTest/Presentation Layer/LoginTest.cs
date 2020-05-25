using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using TimeMate.Controllers;
using TimeMate.Models;
using Xunit;

namespace TimeMateTest.Presentation_Layer
{
    public class LoginTest: IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly LoginPage _loginPage;

        public LoginTest()
        {
            _driver = new ChromeDriver();
            _loginPage = new LoginPage(_driver);
            _loginPage.Navigate();
        }

        [Fact]
        public void LoggingInNoCredentialsTest()
        {
            _loginPage.PopulateMail("");
            _loginPage.PopulatePassword("");

            _loginPage.ClickLogin();

            Assert.Equal("Dit veld is verplicht.", _loginPage.MailErrorMessage);
            Assert.Equal("Dit veld is verplicht.", _loginPage.PasswordErrorMessage);
        }

        [Fact]
        public void LoggingInNoEmailTest()
        {
            _loginPage.PopulateMail("");
            _loginPage.PopulatePassword("euwuur1238dSQ");

            _loginPage.ClickLogin();

            Assert.Equal("Dit veld is verplicht.", _loginPage.MailErrorMessage);
        }

        [Fact]
        public void LoggingInNoPasswordTest()
        {
            _loginPage.PopulateMail("plant@duckduckgo.com");
            _loginPage.PopulatePassword("");

            _loginPage.ClickLogin();

            Assert.Equal("Dit veld is verplicht.", _loginPage.PasswordErrorMessage);
        }

        [Fact]
        public void GoToRegisterViewTest()
        {
            _loginPage.ClickRegister();

            Assert.Equal("Registreren - TimeMate", _loginPage.Title);
            Assert.Contains("Maak een account aan", _loginPage.Source);
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
