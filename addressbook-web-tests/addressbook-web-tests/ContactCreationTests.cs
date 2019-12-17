using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {
        
        [Test]
        public void NewContactTest()
        {
            OpenHomePage();
            Login(new AccountData("admin", "secret"));
            InitContactCreation();
            ContactData contact = new ContactData("User");
            contact.LastName = "Test";
            contact.Address = "Test Address";
            contact.Telephone = "Tel";
            contact.Email = "Email";
            FillContactForm(contact);
            SubmitContactCreation();
            ReturnToHomePage();
            ViewCreatedContactDetails();
        }
    }
}
