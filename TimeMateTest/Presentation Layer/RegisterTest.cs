using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TimeMateTest.Presentation_Layer
{
    public class RegisterTest
    {
        private readonly IWebDriver _driver;
        private readonly RegisterPage _registerPage;

        public RegisterTest()
        {
            _driver = new ChromeDriver();
            _registerPage = new RegisterPage(_driver);
            _registerPage.Navigate();
        }

        [Fact]
        public void RegisterNoCredentialsTest()
        {
            _registerPage.PopulateFirstName("");
            _registerPage.PopulateMail("");
            _registerPage.PopulatePassword("");

            _registerPage.ClickRegister();

            Assert.Equal("Dit veld is verplicht.", _registerPage.FirstNameErrorMessage);
            Assert.Equal("Dit veld is verplicht.", _registerPage.MailErrorMessage);
            Assert.Equal("Dit veld is verplicht.", _registerPage.PasswordErrorMessage);
        }

        [Fact]
        public void RegisterNoFirstNameTest()
        {
            _registerPage.PopulateFirstName("");
            _registerPage.PopulateMail("plant@duckduckgo.com");
            _registerPage.PopulatePassword("Wuu#1238dSQ");

            _registerPage.ClickRegister();

            Assert.Equal("Dit veld is verplicht.", _registerPage.FirstNameErrorMessage);
        }

        [Fact]
        public void RegisterNoMailTest()
        {
            _registerPage.PopulateFirstName("Ben");
            _registerPage.PopulateMail("");
            _registerPage.PopulatePassword("Wuu#1238dSQ");

            _registerPage.ClickRegister();

            Assert.Equal("Dit veld is verplicht.", _registerPage.MailErrorMessage);
        }

        [Fact]
        public void RegisterWrongMailTest()
        {
            _registerPage.PopulateFirstName("");
            _registerPage.PopulateMail("plant@duckduckgo.");
            _registerPage.PopulatePassword("Wuu#1238dSQ");

            _registerPage.ClickRegister();

            Assert.Equal("Voor een geldige E-mailadres in.", _registerPage.MailErrorMessage);
        }

        [Fact]
        public void RegisterNoPasswordTest()
        {
            _registerPage.PopulateFirstName("Ben");
            _registerPage.PopulateMail("plant@duckduckgo.com");
            _registerPage.PopulatePassword("");

            _registerPage.ClickRegister();

            Assert.Equal("Dit veld is verplicht.", _registerPage.PasswordErrorMessage);
        }

        [Fact]
        public void RegisterShortPasswordTest()
        {
            _registerPage.PopulateFirstName("Ben");
            _registerPage.PopulateMail("plant@duckduckgo.com");
            _registerPage.PopulatePassword("QWue23!");

            _registerPage.ClickRegister();

            Assert.Equal("The field Voor een wachtwoord in. must be a string or array type with a minimum length of '9'.", _registerPage.PasswordErrorMessage);
        }

        [Fact]
        public void GoToLoginViewTest()
        {
            _registerPage.ClickBackToLogin();

            Assert.Equal("Inloggen - TimeMate", _registerPage.Title);
            Assert.Contains("Log in", _registerPage.Source);
        }
    }
}
