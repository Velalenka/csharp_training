using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Newtonsoft.Json;
using System.IO;

namespace WebAddressbookTests
{
    public class MovingContactsTests : AuthTestBase
    {
        public static ContactData ContactDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<ContactData>>(
                File.ReadAllText(@"contacts.json")).First();
        }

        public static GroupData GroupDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<GroupData>>(
                File.ReadAllText(@"groups.json")).First();
        }

        public static List<ContactData> GetContactsNotInGroup(GroupData groupData)
        {
            List<ContactData> groupContacts = groupData.GetContacts();
            return ContactData.GetAll().Except(groupContacts).ToList();
        }

        public static GroupData GetValidGroupToAddContact()
        {
            return GroupData.GetAll().Find((GroupData gd) => GetContactsNotInGroup(gd).Count != 0);
        }

        [Test]
        public void AddingContactToGroupTest()
        {
            // adding group if none
            if (GroupData.GetAll().Count == 0)
            {
                app.Groups.Create(GroupDataFromJsonFile());
            }

            GroupData groupData = GetValidGroupToAddContact();

            // adding contact if none
            if (ContactData.GetAll().Count == 0 || groupData == null)
            {
                app.Contacts.Create(ContactDataFromJsonFile());
            }

            GroupData groupToAdd = GetValidGroupToAddContact();
            List<ContactData> oldList = groupToAdd.GetContacts();
            ContactData firstContact = GetContactsNotInGroup(groupToAdd).First();

            app.Contacts.AddContactToGroup(firstContact, groupToAdd);

            List<ContactData> newList = groupToAdd.GetContacts();
            oldList.Add(firstContact);
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }

        [Test]
        public void RemovingContactFromGroupTest()
        {
            GroupData group = GroupData.GetAll().Find((GroupData groupData) =>
            {
                return groupData.GetContacts().Count != 0;
            });

            if (group == null)
            {
                if (GroupData.GetAll().Count == 0)
                {
                    app.Groups.Create(GroupDataFromJsonFile());
                }

                group = GroupData.GetAll().First();

                if (ContactData.GetAll().Count == 0)
                {
                    app.Contacts.Create(ContactDataFromJsonFile());
                }

                app.Contacts.AddContactToGroup(ContactData.GetAll().First(), group);
            }

            List<ContactData> oldList = group.GetContacts();
            ContactData contact = oldList.First();

            app.Contacts.RemoveContactFromGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Remove(contact);
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }
    }
}
