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
            SelectContact(index);
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
            driver.FindElement(By.XPath("(//img[@alt='Edit'])[" + (index + 1) + "]")).Click();
            return this;
        }

        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            driver.SwitchTo().Alert().Accept();
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

        public ContactHelper SelectContact(int index)
        {
            manager.Navigator.OpenHomePage();
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")).Click();
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
            Type(By.Name("home"), contact.Telephone);
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
                ReturnToHomePage();
                ICollection<IWebElement> elements = driver.FindElements(By.Name("entry"));
                foreach (IWebElement element in elements)
                {
                    cells = element.FindElements(By.TagName("td"));
                    ContactCache.Add(new ContactData(cells[2].Text, cells[1].Text));
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
