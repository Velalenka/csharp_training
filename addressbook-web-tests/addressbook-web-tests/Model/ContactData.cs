using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace WebAddressbookTests
{
    [Table(Name = "addressbook")]
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allEmails;
        private string homePhone;
        private string mobilePhone;
        private string workPhone;
        private string email;
        private string email2;
        private string email3;

        private bool checkString = false;

        public ContactData()
        {
        }

        public ContactData(string name, string lastName)
        {
            Name = name;
            LastName = lastName;
        }

        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null)) //сравнение с объектом, который равен null
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other)) //сравнение объекта самого с собой
            {
                return true;
            }
            return Name == other.Name && LastName == other.LastName;
        }

        public override int GetHashCode()
        {
            return (Name + " " + LastName).GetHashCode();
        }

        public override string ToString()
        {
            return "name=" + Name + "\nlastName= " + LastName + "\nmiddleName= " + MiddleName + "\naddress= " + Address + 
                "\nhomePhone= " + HomePhone + "\nmobilePhone= " + MobilePhone +"\nworkPhone= " + WorkPhone + 
                "\nemail= " + Email + "\nemail2= " + Email2 + "\nemail3= " + Email3; //возвращает строковое представление объектов типа GroupData
        }

        public string AllFieldsToString()
        {
            checkString = true;
            return Name + " " + MiddleName + " " + LastName + "\r\n" + Address + "\r\n" + "\r\n" + HomePhone + MobilePhone + WorkPhone + 
                Email + Email2 + Email3;
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }

            if (LastName.CompareTo(other.LastName) == 0)
            {
                return Name.CompareTo(other.Name);
            }

            return LastName.CompareTo(other.LastName);
        }

        [Column(Name = "firstname")]
        public string Name { get; set; }

        [Column(Name = "lastname")]
        public string LastName { get; set; }

        [Column(Name = "middlename")]
        public string MiddleName { get; set; }

        [Column(Name = "address")]
        public string Address { get; set; }

        [Column(Name = "home")]
        public string HomePhone 
        {
            get
            {
                if (checkString)
                {
                    return "H: " + homePhone + "\r\n";
                }
                else
                {
                    return homePhone;
                }
            }
            set
            {
                homePhone = value;
            }
        }

        [Column(Name = "mobile")]
        public string MobilePhone
        {
            get
            {
                if (checkString)
                {
                    return "M: " + mobilePhone + "\r\n";
                }
                else
                {
                    return mobilePhone;
                }
            }
            set
            {
                mobilePhone = value;
            }
        }

        [Column(Name = "work")]
        public string WorkPhone
        {
            get
            {
                if (checkString)
                {
                    return "W: " + workPhone + "\r\n" + "\r\n";
                }
                else
                {
                    return workPhone;
                }
            }
            set
            {
                workPhone = value;
            }
        }

        public string AllPhones
        {
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return (PhoneCleanUp(HomePhone) + PhoneCleanUp(MobilePhone) + PhoneCleanUp(WorkPhone)).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }

        private string PhoneCleanUp(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            else
            {
                return phone.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "") + "\r\n";
            }
        }

        [Column(Name = "email")]
        public string Email
        {
            get
            {
                if (checkString)
                {
                    return email + "\r\n";
                }
                else
                {
                    return email;
                }
            }
            set
            {
                email = value;
            }
        }

        [Column(Name = "email2")]
        public string Email2
        {
            get
            {
                if (checkString)
                {
                    return email2 + "\r\n";
                }
                else
                {
                    return email2;
                }
            }
            set
            {
                email2 = value;
            }
        }

        [Column(Name = "email3")]
        public string Email3
        {
            get
            {
                if (checkString)
                {
                    return email3;
                }
                else
                {
                    return email3;
                }
            }
            set
            {
                email3 = value;
            }
        }

        public string AllEmails
        {
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    return (EmailCleanUp(Email) + EmailCleanUp(Email2) + EmailCleanUp(Email3)).Trim();
                }
            }
            set
            {
                allEmails = value;
            }
        }

        private string EmailCleanUp(string email)
        {
            if (email == null || email == "")
            {
                return "";
            }
            else
            {
                return email.Replace(" ", "") + "\r\n";
            }
        }

        [Column(Name = "id"), PrimaryKey, Identity]
        public string Id { get; set; }

        public static List<ContactData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts select c).ToList();
            }
        }
    }
}