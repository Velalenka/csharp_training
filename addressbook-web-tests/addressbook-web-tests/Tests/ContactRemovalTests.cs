using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {

        [Test]
        public void ContactRemovalTest()
        {
            ContactData newContact = new ContactData("User", "Test");
            newContact.Address = "Test Address";
            newContact.Telephone = "Tel";
            newContact.Email = "Email";

            List<ContactData> oldContacts = app.Contacts.GetContactList();

            if (!app.Contacts.IsContactExists(0))
            {
                app.Contacts.Create(newContact);
            }
            app.Contacts.Remove(0);

            Assert.AreEqual(oldContacts.Count - 1, app.Contacts.GetContactCount());

            List<ContactData> newContacts = app.Contacts.GetContactList();

            oldContacts.RemoveAt(0);
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
