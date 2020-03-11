using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        private ReadOnlyCollection<IWebElement> cells;

        internal ContactData GetContactInforamtionFromTable(int index)
        {
            manager.Navigator.OpenHomePage();

            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"));
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allEmails = cells[4].Text;
            string allPhones = cells[5].Text;

            return new ContactData(firstName, lastName)
            {
                Address = address,
                AllEmails = allEmails,
                AllPhones = allPhones
            };
        }

        internal void AddContactToGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.OpenHomePage();
            ClearGroupFilter();
            SelectContact(contact.Id);
            SelectGroupToAdd(group.Name);
            CommitAddingContactToGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => driver.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        public void RemoveContactFromGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.OpenHomePage();
            SelectGroup(group.Name);
            SelectContact(contact.Id);
            CommitDeletingContactFromGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => driver.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        private void CommitDeletingContactFromGroup()
        {
            driver.FindElement(By.Name("remove")).Click();
        }

        private void SelectGroup(string name)
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText(name);
        }

        public void CommitAddingContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
        }

        public void SelectGroupToAdd(string name)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(name);
        }

        public void SelectContact(string contactId)
        {
            driver.FindElement(By.Id(contactId)).Click();
        }

        public void ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
        }

        internal ContactData GetContactInforamtionFromEditForm(int index)
        {
            manager.Navigator.OpenHomePage();
            InitContactModification(index);
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string middleName = driver.FindElement(By.Name("middlename")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");
            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            return new ContactData(firstName, lastName)
            {
                MiddleName = middleName,
                Address = address,
                Email = email,
                Email2 = email2,
                Email3 = email3,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone
            };
        }

        internal string GetContactInformationFromDetails(int index)
        {
            manager.Navigator.OpenHomePage();
            SelectContactDetails(index);
            return driver.FindElement(By.CssSelector("#content")).Text;
        }

        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }

        public ContactHelper Create(ContactData contact)
        {
            InitContactCreation();
            FillContactForm(contact);
            SubmitContactCreation();
            ReturnToHomePage();
            return this;
        }

        public ContactHelper Remove(int index)
        {
            SelectContactToEdit(index);
            RemoveContact();
            return this;
        }

        public ContactHelper Remove(ContactData contact)
        {
            SelectContactToEdit(contact.Id);
            RemoveContact();
            return this;
        }

        public ContactHelper Modify(int index, ContactData newData)
        {
            InitContactModification(index);
            FillContactForm(newData);
            SubmitContactModification();
            ReturnToHomePage();
            ViewCreatedContactDetails();
            ReturnToHomePage();
            return this;
        }

        public ContactHelper Modify(ContactData contact, ContactData newData)
        {
            InitContactModification(contact.Id);
            FillContactForm(newData);
            SubmitContactModification();
            ReturnToHomePage();
            ViewCreatedContactDetails();
            ReturnToHomePage();
            return this;
        }

        public ContactHelper ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home")).Click();
            return this;
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            ContactCache = null;
            return this;
        }

        public ContactHelper InitContactModification(int index)
        {
            manager.Navigator.OpenHomePage();
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[7]
                .FindElement(By.TagName("a")).Click();
            return this;
        }

        public ContactHelper InitContactModification(String id)
        {
            manager.Navigator.OpenHomePage();
            driver.FindElement(By.XPath("//*[@name='entry']//*[contains(local-name(),'td')][8]//a[contains(@href,'" + id + "')]")).Click();
            return this;
        }


        public ContactHelper SelectContactDetails(int index)
        {
            manager.Navigator.OpenHomePage();
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[6]
                .FindElement(By.TagName("a")).Click();
            return this;
        }

        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            driver.SwitchTo().Alert().Accept();
            ReturnToHomePage();
            ContactCache = null;
            return this;
        }

        public bool IsContactExists(int index)
        {
            if (IsElementPresent(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")))
            {
                return true;
            }
            return false;
        }

        public ContactHelper SelectContactToEdit(int index)
        {
            manager.Navigator.OpenHomePage();
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")).Click();
            return this;
        }

        public ContactHelper SelectContactToEdit(String id)
        {
            manager.Navigator.OpenHomePage();
            driver.FindElement(By.XPath("(//input[@name='selected[]' and @value='" + id + "'])")).Click();
            return this;
        }

        public ContactHelper InitContactCreation()
        {
            manager.Navigator.OpenHomePage();
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }

        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Name);
            Type(By.Name("lastname"), contact.LastName);
            Type(By.Name("address"), contact.Address);
            Type(By.Name("home"), contact.HomePhone);
            Type(By.Name("mobile"), contact.MobilePhone);
            Type(By.Name("work"), contact.WorkPhone);
            Type(By.Name("email"), contact.Email);
            return this;
        }

        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            ContactCache = null;
            return this;
        }

        public void ViewCreatedContactDetails()
        {
            driver.FindElement(By.XPath("//img[@alt='Details']")).Click();
        }

        private List<ContactData> ContactCache = null;

        public List<ContactData> GetContactList()
        {
            if (ContactCache == null)
            {
                ContactCache = new List<ContactData>();
                ICollection<IWebElement> elements = driver.FindElements(By.Name("entry"));
                foreach (IWebElement element in elements)
                {
                    cells = element.FindElements(By.TagName("td"));
                    ContactCache.Add(new ContactData(cells[2].Text, cells[1].Text)
                    {
                        Id = element.FindElement(By.TagName("input")).GetAttribute("value")
                    });
                }
            }
            return new List<ContactData>(ContactCache);
        }

        public int GetContactCount()
        {
            return driver.FindElements(By.Name("entry")).Count;
        }
    }
}
