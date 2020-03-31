using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.Linq;

namespace mantis_project_tests
{
    [TestFixture]
    public class ProjectCreationTests : TestBase
    {
        public static IEnumerable<ProjectData> RandomProjectDataProvider()
        {
            List<ProjectData> projects = new List<ProjectData>();
            for (int i = 0; i < 2; i++)
            {
                projects.Add(new ProjectData(GenerateRandomString(30))
                {
                });
            }
            return projects;
        }

        [Test, TestCaseSource("RandomProjectDataProvider")]
        public void AddNewProjectTest2(ProjectData project)
        {
            AccountData account = new AccountData()
            {
                Username = "administrator",
                Password = "root"
            };

            List<ProjectData> oldProjects = app.API.GetProjectsList(account);
            app.API.CreateNewProject(account, project);

            Assert.AreEqual(oldProjects.Count + 1, app.API.GetProjectsList(account).Count);

            List<ProjectData> newProjects = app.API.GetProjectsList(account);
            oldProjects.Add(project);
            oldProjects.Sort();
            newProjects.Sort();
            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}