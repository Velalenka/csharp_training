using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_project_tests
{
    public class ProjectHelper : HelperBase
    {
        public ProjectHelper(ApplicationManager manager) : base(manager)
        {
        }

        public ProjectHelper Create(ProjectData project)
        {
            manager.Navigator.GoToProjectsPage();
            ClickToCreateProject();
            FillProjectForm(project);
            SubmitProjectCreation();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => driver.FindElements(By.CssSelector("div.widget-toolbox.padding-8.clearfix button")).Count > 0);
            return this;
        }

        public bool IsProjectExists(int index)
        {
            manager.Navigator.GoToProjectsPage();
            if (IsElementPresent(By.XPath("//tr[" + (index + 1) + "]/td/a")))
            {
                return true;
            }
            return false;
        }

        public ProjectHelper SubmitProjectCreation()
        {
            driver.FindElement(By.CssSelector("div.widget-toolbox.padding-8.clearfix > input")).Click();
            ProjectCache = null;
            return this;
        }

        public ProjectHelper FillProjectForm(ProjectData project)
        {
            Type(By.Id("project-name"), project.Name);
            return this;
        }

        public ProjectHelper ClickToCreateProject()
        {
            driver.FindElement(By.CssSelector("div.widget-toolbox.padding-8.clearfix button")).Click();
            return this;
        }

        public ProjectHelper Remove(int index)
        {
            manager.Navigator.GoToProjectsPage();
            SelectProject(index);
            RemoveProject();
            ProjectCache = null;
            return this;
        }

        public ProjectHelper RemoveProject()
        {
            driver.FindElement(By.Id("project-delete-form")).Click();
            driver.FindElement(By.XPath("//input[@value='Delete Project']")).Click();
            return this;
        }

        public ProjectHelper SelectProject(int index)
        {
            driver.FindElement(By.XPath("//tr[" + (index+1) + "]/td/a")).Click();
            return this;
        }

        public int GetProjectCount()
        {
            return driver.FindElements(By.CssSelector("td > a")).Count;
        }

        private List<ProjectData> ProjectCache = null;

        public List<ProjectData> GetProjectsList()
        {
            if (ProjectCache == null)
            {
                ProjectCache = new List<ProjectData>();
                manager.Navigator.GoToProjectsPage();
                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("td > a"));
                foreach (IWebElement element in elements)
                {
                    ProjectCache.Add(new ProjectData(element.Text));
                }
            }
            return new List<ProjectData>(ProjectCache);
        }
    }
}
