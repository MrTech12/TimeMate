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
        private readonly ChromeOptions options = new ChromeOptions();

        public RegisterTest()
        {
            options.AddArgument("--headless");
            _driver = new ChromeDriver(options);
            _registerPage = new RegisterPage(_driver);
            _registerPage.Navigate();
        }

        [Fact]
        public void RegisterNoCredentials()
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
        public void RegisterNoFirstName()
        {
            _registerPage.PopulateFirstName("");
            _registerPage.PopulateMail("plant@duckduckgo.com");
            _registerPage.PopulatePassword("Wuu#1238dSQ");

            _registerPage.ClickRegister();

            Assert.Equal("Dit veld is verplicht.", _registerPage.FirstNameErrorMessage);
        }

        [Fact]
        public void RegisterNoMail()
        {
            _registerPage.PopulateFirstName("Ben");
            _registerPage.PopulateMail("");
            _registerPage.PopulatePassword("Wuu#1238dSQ");

            _registerPage.ClickRegister();

            Assert.Equal("Dit veld is verplicht.", _registerPage.MailErrorMessage);
        }

        [Fact]
        public void RegisterWrongMail()
        {
            _registerPage.PopulateFirstName("");
            _registerPage.PopulateMail("plant@duckduckgo.");
            _registerPage.PopulatePassword("Wuu#1238dSQ");

            _registerPage.ClickRegister();

            Assert.Equal("Voor een geldig E-mailadres in.", _registerPage.MailErrorMessage);
        }

        [Fact]
        public void RegisterNoPassword()
        {
            _registerPage.PopulateFirstName("Ben");
            _registerPage.PopulateMail("plant@duckduckgo.com");
            _registerPage.PopulatePassword("");

            _registerPage.ClickRegister();

            Assert.Equal("Dit veld is verplicht.", _registerPage.PasswordErrorMessage);
        }

        [Fact]
        public void RegisterShortPassword()
        {
            _registerPage.PopulateFirstName("Ben");
            _registerPage.PopulateMail("plant@duckduckgo.com");
            _registerPage.PopulatePassword("QWue23!");

            _registerPage.ClickRegister();

            Assert.Equal("The field Voor een wachtwoord in. must be a string or array type with a minimum length of '9'.", _registerPage.PasswordErrorMessage);
        }

        [Fact]
        public void RegisterLowerCasePassword()
        {
            _registerPage.PopulateFirstName("Ben");
            _registerPage.PopulateMail("plant@duckduckgo.com");
            _registerPage.PopulatePassword("qweeeweue23!");

            _registerPage.ClickRegister();

            Assert.Equal("Het wachtwoord moet een hoofdletter bevatten.", _registerPage.ValidationSummaryErrorMessage);
        }

        [Fact]
        public void RegisterNoSpecialCharactersPassword()
        {
            _registerPage.PopulateFirstName("Ben");
            _registerPage.PopulateMail("plant@duckduckgo.com");
            _registerPage.PopulatePassword("EEweeeweue23");

            _registerPage.ClickRegister();

            Assert.Equal("Het wachtwoord moet een speciale karakter bevatten.", _registerPage.ValidationSummaryErrorMessage);
        }

        [Fact]
        public void RegisterNoNumberPassword()
        {
            _registerPage.PopulateFirstName("Ben");
            _registerPage.PopulateMail("plant@duckduckgo.com");
            _registerPage.PopulatePassword("qweEeweu!!ee");

            _registerPage.ClickRegister();

            Assert.Equal("Het wachtwoord moet een cijfer bevatten.", _registerPage.ValidationSummaryErrorMessage);
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
