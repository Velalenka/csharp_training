using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {

        [Test]
        public void ContactRemovalTest()
        {
            ContactData newContact = new ContactData("User");
            newContact.LastName = "Test";
            newContact.Address = "Test Address";
            newContact.Telephone = "Tel";
            newContact.Email = "Email";

            if (!app.Contacts.IsContactExists(1))
            {
                app.Contacts.Create(newContact);
            }
            app.Contacts.Remove(1);
        }
    }
}
