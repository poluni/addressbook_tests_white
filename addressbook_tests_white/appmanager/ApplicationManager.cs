using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TestStack.White;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems;

namespace addressbook_tests_white
{
    public class ApplicationManager
    {
        public static string WINTITLE = "Free Address Book";
        private GroupHelper groupHelper;
        private ContactHelper contactHelper;

        public ApplicationManager()
        {
            Application app = Application.Launch(@"D:\FreeAddressBookPortable\AddressBook.exe"); //путь до тестируемого приложения
            groupHelper = new GroupHelper(this);
            contactHelper = new ContactHelper(this);
            MainWindow = app.GetWindow(WINTITLE);
        }

        public void Stop()
        {
            MainWindow.Get<Button>("uxExitAddressButton").Click();
        }

        public Window MainWindow  {get; private set;}

        public GroupHelper Groups
        {
            get { return groupHelper; }
        }


        public ContactHelper Contacts
        {
            get { return contactHelper; }
        }

    }
}
