using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_project_tests
{
    public class LoginHelper : HelperBase
    {
        public LoginHelper(ApplicationManager manager) : base(manager)
        {
        }

        internal void Login(AccountData account)
        {
            if (IsLoggedIn())
            {
                if (IsLoggedIn(account))
                {
                    return;
                }

                Logout();
            }
            FillUsername(account);
            Submit();
            FillPassword(account);
            Submit();
        }

        public void Logout()
        {
            if (IsLoggedIn())
            {
                driver.FindElement(By.CssSelector("span.user-info")).Click();
                driver.FindElement(By.CssSelector("ul.user-menu li:nth-child(4)")).Click();
            }
        }

        public bool IsLoggedIn()
        {
            return IsElementPresent(By.CssSelector("span.user-info"));
        }

        public bool IsLoggedIn(AccountData account)
        {
            return IsLoggedIn()
                && GetLoggedUserName() == account.Username;
        }

        public string GetLoggedUserName()
        {
            string text = driver.FindElement(By.CssSelector("span.user-info")).Text;
            return text.Substring(1, text.Length - 2);
        }

        private void FillPassword(AccountData account)
        {
            driver.FindElement(By.Name("password")).SendKeys(account.Password);
        }

        private void Submit()
        {
            driver.FindElement(By.CssSelector("input.btn")).Click();
        }

        private void FillUsername(AccountData account)
        {
            driver.FindElement(By.Name("username")).SendKeys(account.Username);
        }
    }
}
