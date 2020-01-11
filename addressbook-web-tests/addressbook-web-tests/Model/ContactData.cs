using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {

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
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return Name; //возвращает строковое представление объектов типа GroupData
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

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Telephone { get; set; }

        public string Email { get; set; }

        public string Id { get; set; }
    }
}