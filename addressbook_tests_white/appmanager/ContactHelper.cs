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

        public TableRows GetContactsDataFromTable()
        {
            
            Table table = manager.MainWindow.Get<Table>("uxAddressGrid");
            return table.Rows;
        }

        public List<ContactData> GetContactsList()
        {
            {
                List<ContactData> list = new List<ContactData>();
                TableRows rows = GetContactsDataFromTable();
                for (int i = 0; i < rows.Count; i++)
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

        public void Remove(int num, bool alertAcc)
        {
            SelectContact(num);
            Window dialogForm = InitContactRemoval();
            CloseAlertAndGetItsText(dialogForm, alertAcc);
        }

        private Window InitContactRemoval()
        {
            manager.MainWindow.Get<Button>("uxDeleteAddressButton").Click();
            return manager.MainWindow.ModalWindow("Question");
        }

        private void SelectContact(int num)
        {
            GetContactsDataFromTable()[num].Select();
        }

        private void CloseAlertAndGetItsText(Window dialogForm, bool alertAcc)
        {
            Panel btnPanel = dialogForm.Get<Panel>("uxContainerTableLayoutPanel").Get<Panel>("uxButtonsFlowLayoutPanel");
            string txtBtn = btnPanel.Get(SearchCriteria.ByControlType(ControlType.Button)).Name;
            if (alertAcc)
            {
                switch (txtBtn)
                { 
                    case "Yes":
                        btnPanel.Get(SearchCriteria.ByControlType(ControlType.Button).AndByText("Yes")).Click();
                        break;
                    case "No":
                        btnPanel.Get(SearchCriteria.ByControlType(ControlType.Button).AndByText("Yes")).Focus();
                        btnPanel.Get(SearchCriteria.ByControlType(ControlType.Button).AndByText("Yes")).Click();
                        break;
                }
            }
            else
            {
                switch (txtBtn)
                {
                    case "Yes":
                        btnPanel.Get(SearchCriteria.ByControlType(ControlType.Button).AndByText("No")).Focus();
                        btnPanel.Get(SearchCriteria.ByControlType(ControlType.Button).AndByText("No")).Click();
                        break;                        
                    case "No":
                        btnPanel.Get(SearchCriteria.ByControlType(ControlType.Button).AndByText("No")).Click();
                        break;
                }
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
            Panel tabPanelRight = tabPanel.Get<Panel>("uxRightGasPanel");
            TextBox textboxFN = tabPanelLeft.Get<TextBox>("ueFirstNameAddressTextBox");
            textboxFN.Enter(newContact.Firstname);
            TextBox textboxLN = tabPanelLeft.Get<TextBox>("ueLastNameAddressTextBox");
            textboxLN.Enter(newContact.Lastname);
            CheckBox cmpFlag = tabPanelRight.Get<CheckBox>("uxIsCompanyGasCheckBox");
            cmpFlag.Select();
            if (cmpFlag.IsSelected)
            {
                TextBox textboxCom = tabPanelRight.Get<TextBox>("ueCompanyAddressTextBox");
                textboxCom.Enter(newContact.Company);
            }            
            TextBox textboxCity = tabPanelRight.Get<TextBox>("ueCityAddressTextBox");
            textboxCity.Enter(newContact.City);
            TextBox textboxAdress = tabPanelRight.Get<TextBox>("ueAddressAddressTextBox");
            textboxAdress.Enter(newContact.Address);
        }

        private void CloseContactDialogue(Window dialogue)
        {
            dialogue.Get<Button>("uxSaveAddressButton").Click();
        }

        public void CheckContactExist(int num, ContactData contactData)
        {
            if (!IsContactCreatedBase())
            {
                if (!IsContactCreated(num, contactData))
                {
                    AddNewContact(contactData);
                }
            }
        }

        public bool IsContactCreated(int num, ContactData contact)
        {
            return IsContactCreatedBase()
                && GetContactsDataFromTable()[num].Cells[0].ToString()
                == contact.Firstname;
        }

        public bool IsContactCreatedBase()
        {
            return GetContactsDataFromTable().Count != 0 ;
        }
    }
}
