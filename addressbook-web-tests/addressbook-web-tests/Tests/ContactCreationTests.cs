using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            List<ContactData> contacts = new List<ContactData>();
            for (int i = 0; i < 1; i++)
            {
                contacts.Add(new ContactData(GenerateRandomString(10), GenerateRandomString(35))
                {
                    MiddleName = GenerateRandomString(15),
                    Address = GenerateRandomString(20),
                    HomePhone = GenerateRandomString(10),
                    MobilePhone = GenerateRandomString(10),
                    WorkPhone = GenerateRandomString(10),
                    Email = GenerateRandomString(10),
                    Email2 = GenerateRandomString(10),
                    Email3 = GenerateRandomString(10)
                });
            }
            return contacts;
        }

        [Test, TestCaseSource("RandomContactDataProvider")]
        public void NewContactTest(ContactData contact)
        {
            //ContactData contact = new ContactData("User", "Test LastName");
            //contact.Address = "Test Address";
            //contact.HomePhone = "+37529-888";
            //contact.MobilePhone = "37529-889";
            //contact.WorkPhone = "37529-887";
            //contact.Email = "Email";

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
