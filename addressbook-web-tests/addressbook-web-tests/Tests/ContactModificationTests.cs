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

            app.Contacts.Modify(1, newData);
        }
    }
}
