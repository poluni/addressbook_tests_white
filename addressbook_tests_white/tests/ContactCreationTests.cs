using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace addressbook_tests_white
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {
        [Test]
        public void TestContactCreation()
        {
           List<ContactData> oldContacts = app.Contacts.GetContactsList();
            ContactData newContact = new ContactData()
            {
                Firstname = "A1", 
                Lastname = "B1",
            };
            app.Contacts.AddNewContact(newContact);
            List<ContactData> newContacts = app.Contacts.GetContactsList();
            oldContacts.Add(newContact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
