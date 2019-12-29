using System;
using System.Collections.Generic;
using TestStack.White;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.TreeItems;
using System.Windows.Automation;
using TestStack.White.InputDevices;
using TestStack.White.WindowsAPI;
using TestStack.White.UIItems.ListViewItems;
using TestStack.White.UIItems.TableItems;
using TestStack.White.UIItems.TabItems;

namespace addressbook_tests_white
{
    public class ContactHelper : HelperBase
    {
        public static string CONTACTWINTITLE = "Contact Editor";
        public static string TABTITLE = "General details";

        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }

        public List<ContactData> GetContactsList()
        {
            {
                List<ContactData> list = new List<ContactData>();
                Table table = manager.MainWindow.Get<Table>("uxAddressGrid");
                TableRows rows = table.Rows;
                int rowsCount = rows.Count;
                for (int i = 0; i < rowsCount; i++)
                {
                    string firstname = rows[i].Cells[0].Value.ToString().Contains("не определено") ? 
                                          null : rows[i].Cells[0].Value.ToString();
                    string lastname = rows[i].Cells[1].Value.ToString().Contains("не определено") ?
                                          null : rows[i].Cells[1].Value.ToString();
                    string company = rows[i].Cells[2].Value.ToString().Contains("не определено") ?
                                          null : rows[i].Cells[2].Value.ToString();
                    string city = rows[i].Cells[3].Value.ToString().Contains("не определено") ?
                                          null : rows[i].Cells[3].Value.ToString();
                    string address = rows[i].Cells[4].Value.ToString().Contains("не определено") ?
                                          null : rows[i].Cells[4].Value.ToString();

                    list.Add(new ContactData
                    {
                        Firstname = firstname,
                        Lastname = lastname,
                        Company = company,
                        City = city,
                        Address = address
                    });
                }
                return list;
            }
        }

        public void AddNewContact(ContactData newContact)
        {
            Window dialogue = OpenContactDialogue();
            SelectTab(dialogue, 0);
            FillContactForm(newContact, dialogue);
            CloseContactDialogue(dialogue);
        }

        public Window OpenContactDialogue()
        {
            manager.MainWindow.Get<Button>("uxNewAddressButton").Click();
            return manager.MainWindow.ModalWindow(CONTACTWINTITLE);
        }

        private void SelectTab(Window dialogue, int tabNum)
        {
            Tab tabPanel = dialogue.Get<Tab>("addressTabControl1");
            tabPanel.SelectTabPage(tabNum);            
        }

        private void FillContactForm(ContactData newContact, Window dialogue)
        {
            Panel tabPanel = dialogue.Get<Panel>("gasTableLayoutPanel1");
            Panel tabPanelLeft = tabPanel.Get<Panel>("uxLeftGasPanel");
            TextBox textboxFN = tabPanelLeft.Get<TextBox>("ueFirstNameAddressTextBox");
            //TextBox textboxFN = (TextBox)tabPanelLeft.Get(SearchCriteria.ByControlType(ControlType.Edit).AndByClassName("First name:"));
            textboxFN.Enter(newContact.Firstname);
            //TextBox textboxLN = (TextBox)tabPanelLeft.Get(SearchCriteria.ByControlType(ControlType.Edit).AndByClassName("Last name:"));
            TextBox textboxLN = tabPanelLeft.Get<TextBox>("ueLastNameAddressTextBox");
            textboxLN.Enter(newContact.Lastname);
        }

        private void CloseContactDialogue(Window dialogue)
        {
            dialogue.Get<Button>("uxSaveAddressButton").Click();
        }
     }
}
