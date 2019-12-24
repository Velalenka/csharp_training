using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {

        [Test]
        public void ContactRemovalTest()
        {
            ContactData contact = new ContactData("User");
            contact.LastName = "Test";
            contact.Address = "Test Address";
            contact.Telephone = "Tel";
            contact.Email = "Email";

            app.Contacts.CheckContactPresence(1, contact);
            app.Contacts.Remove(1);
        }
    }
}
