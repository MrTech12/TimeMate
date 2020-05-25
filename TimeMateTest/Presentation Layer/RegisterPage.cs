using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimeMateTest.Presentation_Layer
{
    class RegisterPage
    {
        private readonly IWebDriver _driver;
        private const string URI = "https://localhost:44329/Account/Register";

        private IWebElement FirstNameElement => _driver.FindElement(By.Id("FirstName"));
        private IWebElement MailElement => _driver.FindElement(By.Id("Mail"));
        private IWebElement PasswordElement => _driver.FindElement(By.Id("Password"));
        private IWebElement RegisterElement => _driver.FindElement(By.Id("registerButton"));
        private IWebElement GoToLoginElement => _driver.FindElement(By.Id("goLoginButton"));

        public string Title => _driver.Title;
        public string Source => _driver.PageSource;
        public string FirstNameErrorMessage => _driver.FindElement(By.Id("FirstName-error")).Text;
        public string MailErrorMessage => _driver.FindElement(By.Id("Mail-error")).Text;
        public string PasswordErrorMessage => _driver.FindElement(By.Id("Password-error")).Text;

        public RegisterPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Navigate() => _driver.Navigate()
           .GoToUrl(URI);

        public void PopulateFirstName(string mail) => FirstNameElement.SendKeys(mail);
        public void PopulateMail(string mail) => MailElement.SendKeys(mail);
        public void PopulatePassword(string password) => PasswordElement.SendKeys(password);
        public void ClickRegister() => RegisterElement.Click();
        public void ClickBackToLogin() => GoToLoginElement.Click();
    }
}
