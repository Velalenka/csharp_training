using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {
        
        [Test]
        public void NewContactTest()
        {
            ContactData contact = new ContactData("User");
            contact.LastName = "Test";
            contact.Address = "Test Address";
            contact.Telephone = "Tel";
            contact.Email = "Email";

            app.Contacts.Create(contact);
            app.Navigator.ReturnToHomePage();
            app.Contacts.ViewCreatedContactDetails();
        }
    }
}
