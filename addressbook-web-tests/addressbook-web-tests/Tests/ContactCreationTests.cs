using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {
        
        [Test]
        public void NewContactTest()
        {
            app.Navigator.OpenHomePage();
            app.Auth.Login(new AccountData("admin", "secret"));
            app.Contacts.InitContactCreation();
            ContactData contact = new ContactData("User");
            contact.LastName = "Test";
            contact.Address = "Test Address";
            contact.Telephone = "Tel";
            contact.Email = "Email";
            app.Contacts.FillContactForm(contact);
            app.Contacts.SubmitContactCreation();
            app.Navigator.ReturnToHomePage();
            app.Contacts.ViewCreatedContactDetails();
        }
    }
}
