using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_tests_white
{
    public class ContactData : IComparable<ContactData>, IEquatable<ContactData>
    {
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Company { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string Id { get; set; }

        public ContactData()
        {
        }

        public int CompareTo(ContactData otherContact)
        {
            if (Object.ReferenceEquals(otherContact, null))
            {
                return 1;
            }
            if (Lastname.CompareTo(otherContact.Lastname) == 0)
            {
                if (Firstname.CompareTo(otherContact.Firstname) == 0)
                {
                    return Firstname.CompareTo(otherContact.Firstname);
                }
            }
            return Lastname.CompareTo(otherContact.Lastname);
        }

        public bool Equals(ContactData otherContact)
        {
            if (Object.ReferenceEquals(otherContact, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, otherContact))
            {
                return true;
            }
            return Firstname == otherContact.Firstname && Lastname == otherContact.Lastname;
        }


        public override string ToString() => "Id = " + Id
            + "Firstname = " + Firstname 
            + "Lastname  = " + Lastname
            + "Company = " + Company
            + "City = " + City
            + "Address = " + Address;
    }
}
