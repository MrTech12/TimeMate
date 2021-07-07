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
        private IWebElement AddJobElement => _driver.FindElement(By.Id("add-job-fields"));
        private IWebElement RemoveJobElement => _driver.FindElement(By.Id("remove-job-fields"));
        private IWebElement FirstJobHourlyWageElement => _driver.FindElement(By.Name("JobHourlyWage[0]"));
        private IWebElement SecondJobHourlyWageElement => _driver.FindElement(By.Name("JobHourlyWage[1]"));
        private IWebElement RegisterElement => _driver.FindElement(By.Id("register-button"));
        private IWebElement GoToLoginElement => _driver.FindElement(By.Id("go-login-button"));

        public string Title => _driver.Title;
        public string Source => _driver.PageSource;
        public string SecondJobDayType => _driver.FindElement(By.Name("JobDayType[1]")).GetAttribute("value");
        public bool RegisterButtonActive => _driver.FindElement(By.Id("register-button")).Enabled;
        public string FirstNameErrorMessage => _driver.FindElement(By.Id("FirstName-error")).Text;
        public string MailErrorMessage => _driver.FindElement(By.Id("Mail-error")).Text;
        public string PasswordErrorMessage => _driver.FindElement(By.Id("Password-error")).Text;
        public string ValidationSummaryErrorMessage => _driver.FindElement(By.ClassName("validation-summary-errors")).Text;
        public string HourlyWageErrorMessage => _driver.FindElement(By.Id("hourlywage-error")).Text;

        public RegisterPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Navigate() => _driver.Navigate()
           .GoToUrl(URI);

        public void PopulateFirstName(string firstname) => FirstNameElement.SendKeys(firstname);
        public void PopulateMail(string mail) => MailElement.SendKeys(mail);
        public void PopulatePassword(string password) => PasswordElement.SendKeys(password);
        public void ClickAddJob() => AddJobElement.Click();
        public void ClickRemoveJob() => RemoveJobElement.Click();
        public void PopulateFirstJobHourlyWage(string hourlyWage) => FirstJobHourlyWageElement.SendKeys(hourlyWage);
        public void PopulateSecondJobHourlyWage(string hourlyWage) => SecondJobHourlyWageElement.SendKeys(hourlyWage);
        public void ClickRegister() => RegisterElement.Click();
        public void ClickBackToLogin() => GoToLoginElement.Click();
    }
}
