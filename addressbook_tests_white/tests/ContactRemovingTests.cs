using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace addressbook_tests_white
{
    [TestFixture]
    public class ContactRemovalTests : TestBase
    {
        [Test]
        public void TestContactRemoving()
        {
            app.Contacts.CheckContactExist(0, new ContactData()
            {
                Firstname = "A1",
                Lastname = "B1",
                Company = "C1",
                City = "D1",
                Address = "E1"
            });
            List<ContactData> oldContacts = app.Contacts.GetContactsList();
            app.Contacts.Remove(0, true);
            List<ContactData> newContacts = app.Contacts.GetContactsList();
            oldContacts.RemoveAt(0);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
