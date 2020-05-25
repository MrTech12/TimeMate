using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimeMateTest.Presentation_Layer
{
    class LoginPage
    {
        private readonly IWebDriver _driver;
        private const string URI = "https://localhost:44329/Account/Index";

        private IWebElement MailElement => _driver.FindElement(By.Id("Mail"));
        private IWebElement PasswordElement => _driver.FindElement(By.Id("Password"));
        private IWebElement LoginElement => _driver.FindElement(By.Id("loginButton"));
        private IWebElement GoToRegisterElement => _driver.FindElement(By.Id("goRegisterButton"));

        public string Title => _driver.Title;
        public string Source => _driver.PageSource;
        public string MailErrorMessage => _driver.FindElement(By.Id("Mail-error")).Text;
        public string PasswordErrorMessage => _driver.FindElement(By.Id("Password-error")).Text;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Navigate() => _driver.Navigate()
           .GoToUrl(URI);

        public void PopulateMail(string mail) => MailElement.SendKeys(mail);
        public void PopulatePassword(string password) => PasswordElement.SendKeys(password);
        public void ClickLogin() => LoginElement.Click();
        public void ClickRegister() => GoToRegisterElement.Click();
    }
}
