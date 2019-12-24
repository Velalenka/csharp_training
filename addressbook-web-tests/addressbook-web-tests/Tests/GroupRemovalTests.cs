using NUnit.Framework;

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

            if (!app.Groups.IsGroupExists(1))
            {
                app.Groups.Create(group);
            }
            app.Groups.Remove(1);
        }
    }
}
