using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace addressbook_tests_autoit
{
    [TestFixture]
    public class GroupRemovalTests : TestBase
    {
        [Test]
        public void TestGroupRemoval()
        {
            List<GroupData> oldGroups = app.Groups.GetGroupsList();
            int toBeRemoved = 1;

            if (oldGroups.Count <= 1)
            {
                GroupData newGroup = new GroupData()
                {
                    Name = "test"
                };
                app.Groups.Add(newGroup);
                oldGroups.Add(newGroup);
            }

            app.Groups.Remove(toBeRemoved);

            List<GroupData> newGroups = app.Groups.GetGroupsList();

            oldGroups.RemoveAt(toBeRemoved);
            oldGroups.Sort();
            newGroups.Sort();

            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}
