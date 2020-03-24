using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_project_tests
{
    public class NavigationHelper : HelperBase
    {
        private string baseURL;

        public NavigationHelper(ApplicationManager manager, string baseURL) : base(manager)
        {
            this.baseURL = baseURL;
        }

        public void OpenHomePage()
        {
            if (driver.Url == baseURL + "/mantisbt-2.24.0/account_page.php")
            {
                return;
            }
            driver.Navigate().GoToUrl(baseURL + "/mantisbt-2.24.0/account_page.php");
        }

        public void GoToProjectsPage()
        {
            if (driver.Url == baseURL + "/mantisbt-2.24.0/manage_proj_page.php"
                && IsElementPresent(By.CssSelector("div.widget-toolbox.padding-8.clearfix button")))
            {
                return;
            }
            driver.FindElement(By.CssSelector("a[href$='mantisbt-2.24.0/manage_overview_page.php']")).Click();
            driver.FindElement(By.LinkText("Manage Projects")).Click();
        }
    }
}
