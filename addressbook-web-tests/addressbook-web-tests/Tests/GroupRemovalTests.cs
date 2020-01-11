using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {

        [Test]
        public void GroupRemovalTest()
        {
            GroupData group = new GroupData("test");
            group.Header = "test header";
            group.Footer = "test footer";

            List<GroupData> oldGroups = app.Groups.GetGroupList();

            if (!app.Groups.IsGroupExists(0))
            {
                app.Groups.Create(group);
            }
            app.Groups.Remove(0);

            Assert.AreEqual(oldGroups.Count - 1, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();

            oldGroups.RemoveAt(0);
            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}
