using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        
        [Test]
        public void NewContactTest()
        {
            ContactData contact = new ContactData("User", "Test LastName");
            contact.Address = "Test Address";
            contact.HomePhone = "+37529-888";
            contact.MobilePhone = "37529-889";
            contact.WorkPhone = "37529-887";
            contact.Email = "Email";

            List<ContactData> oldContacts = app.Contacts.GetContactList();

            app.Contacts.Create(contact);

            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactCount());

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
