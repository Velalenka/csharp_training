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
            ContactData newData = new ContactData("User Modified");
            newData.LastName = "Test Modified";
            newData.Address = "Test Address Modified";
            newData.Telephone = "Tel Modified";
            newData.Email = "Email Modified";

            ContactData newContact = new ContactData("User");
            newContact.LastName = "Test";
            newContact.Address = "Test Address";
            newContact.Telephone = "Tel";
            newContact.Email = "Email";

            app.Contacts.CheckContactPresence(1, newContact);
            app.Contacts.Modify(1, newData);
        }
    }
}
