using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactInformationTests : AuthTestBase
    {
        [Test]
        public void TestContactInformationInFormAndTable()
        {
            ContactData fromTable = app.Contacts.GetContactInforamtionFromTable(0);
            ContactData fromForm = app.Contacts.GetContactInforamtionFromEditForm(0);

            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
            Assert.AreEqual(fromTable.AllEmails, fromForm.AllEmails);
        }

        [Test]
        public void TestContactInformationInDetailsAndForm()
        {
            string fromDetails = app.Contacts.GetContactInformationFromDetails(0);
            ContactData fromForm = app.Contacts.GetContactInforamtionFromEditForm(0);

            Assert.AreEqual(fromDetails, fromForm.AllFieldsToString());
        }
    }
}
