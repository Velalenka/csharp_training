using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace mantis_project_tests
{
    public class ApplicationManager
    {
        protected IWebDriver driver;
        protected string baseURL;
        protected NavigationHelper navigator;

        protected LoginHelper loginHelper;

        public APIHelper API { get; set; }

        protected ProjectHelper projectHelper;

        private static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();

        private ApplicationManager()
        {
            driver = new FirefoxDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            baseURL = "http://localhost";

            navigator = new NavigationHelper(this, baseURL);
            projectHelper = new ProjectHelper(this);
            loginHelper = new LoginHelper(this);
            API = new APIHelper(this);
        }

        ~ApplicationManager()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        public static ApplicationManager GetInstance()
        {
            if (!app.IsValueCreated)
            {
                ApplicationManager newInstance = new ApplicationManager();
                newInstance.driver.Url = "http://localhost/mantisbt-2.24.0/login_page.php";
                app.Value = newInstance;

            }
            return app.Value;
        }

        public IWebDriver Driver
        {
            get
            {
                return driver;
            }
        }

        public ProjectHelper Projects
        {
            get
            {
                return projectHelper;
            }
        }

        public LoginHelper Auth
        {
            get
            {
                return loginHelper;
            }
        }

        public NavigationHelper Navigator
        {
            get
            {
                return navigator;
            }
        }
    }
}
