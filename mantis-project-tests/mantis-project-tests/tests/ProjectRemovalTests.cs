using NUnit.Framework;
using System.Collections.Generic;

namespace mantis_project_tests
{
    [TestFixture]
    public class ProjectRemovalTests : AuthTestBase
    {
        public static IEnumerable<ProjectData> RandomProjectDataProvider()
        {
            List<ProjectData> projects = new List<ProjectData>();
            for (int i = 0; i < 5; i++)
            {
                projects.Add(new ProjectData(GenerateRandomString(30))
                {
                });
            }
            return projects;
        }

        [Test, TestCaseSource("RandomProjectDataProvider")]
        public void TestProjectRemoval(ProjectData project)
        {
            List<ProjectData> oldProjects = app.Projects.GetProjectsList();

            if (!app.Projects.IsProjectExists(0))
            {
                app.Projects.Create(project);
            }
            app.Projects.Remove(0);

            List<ProjectData> newProjects = app.Projects.GetProjectsList();

            if (oldProjects.Count > 0)
            {
                oldProjects.RemoveAt(0);
            }
            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}