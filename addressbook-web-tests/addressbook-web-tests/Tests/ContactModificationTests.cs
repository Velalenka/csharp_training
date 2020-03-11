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
            newData.HomePhone = "+37529-000";
            newData.MobilePhone = "37529-001";
            newData.WorkPhone = "37529-002";
            newData.Email = "Email Modified";

            ContactData newContact = new ContactData("User", "Test");
            newContact.Address = "Test Address";
            newContact.HomePhone = "+37529-888";
            newContact.MobilePhone = "37529-889";
            newContact.WorkPhone = "37529-887";
            newContact.Email = "Email";

            List<ContactData> oldContacts = ContactData.GetAll();
            ContactData oldData = oldContacts[0];

            if (!app.Contacts.IsContactExists(0))
            {
                app.Contacts.Create(newContact);
            }
            app.Contacts.Modify(oldData, newData);

            Assert.AreEqual(oldContacts.Count, app.Contacts.GetContactCount());

            List<ContactData> newContacts = ContactData.GetAll();
            oldContacts[0].Name = newData.Name;
            oldContacts[0].LastName = newData.LastName;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                if (contact.Id == oldData.Id)
                {
                    Assert.AreEqual(newData.Name, contact.Name);
                }
            }
        }
    }
}
