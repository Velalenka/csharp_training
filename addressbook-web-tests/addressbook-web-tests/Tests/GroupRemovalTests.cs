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

            app.Groups.CheckGroupPresence(1, group);
            app.Groups.Remove(1);
        }
    }
}
