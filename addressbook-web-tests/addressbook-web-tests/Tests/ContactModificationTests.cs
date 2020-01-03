using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [Test]

        public void ContactModificationTest()
        {
            ContactData newData = new ContactData("User Modified", "Test Modified");
            newData.Address = "Test Address Modified";
            newData.Telephone = "Tel Modified";
            newData.Email = "Email Modified";

            ContactData newContact = new ContactData("User", "Test");
            newContact.Address = "Test Address";
            newContact.Telephone = "Tel";
            newContact.Email = "Email";

            List<ContactData> oldContacts = app.Contacts.GetContactList();

            if (!app.Contacts.IsContactExists(0))
            {
                app.Contacts.Create(newContact);
            }
            app.Contacts.Modify(0, newData);

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts[0].Name = newData.Name;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
