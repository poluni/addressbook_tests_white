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

namespace addressbook_tests_white
{
    public class GroupHelper: HelperBase
    {
        public static string GROUPWINTITLE = "Group editor";
        public static string DELGROUPWINTITLE = "Delete group";

        public GroupHelper(ApplicationManager manager) : base(manager)
        {
        }

        public void Add(GroupData newGroup)
        {
            Window dialogue = OpenGroupsDialogue();
            dialogue.Get<Button>("uxNewAddressButton").Click();
            TextBox textbox = (TextBox)dialogue.Get(SearchCriteria.ByControlType(ControlType.Edit));
            textbox.Enter(newGroup.Name);
            Keyboard.Instance.PressSpecialKey(KeyboardInput.SpecialKeys.RETURN);
            CloseGroupsDialogue(dialogue);
        }

        public void Remove(int index)
        {
            Window dialogue = OpenGroupsDialogue();
            //SearchTreeItem(index, dialogue);
            SelectGroup(index, dialogue);
            dialogue.Get<Button>("uxDeleteAddressButton").Click();
            dialogue.Get<Button>("uxOKAddressButton").Click();
            CloseGroupsDialogue(dialogue);
        }

        public GroupHelper CheckGroupExist(int num, GroupData newGroupData)
        {
            Window dialogue = OpenGroupsDialogue();
            if (IsGroupCreated(num, dialogue))
            {                
                CloseGroupsDialogue(dialogue);
                return this;
            }
            else
            {
                CloseGroupsDialogue(dialogue);
                Add(newGroupData);
                return this;
            }
        }

        private bool IsGroupCreated(int num, Window dialogue)
        {
            return (GetItemGroupCount(dialogue) > 1 && num <= GetItemGroupCount(dialogue) ? true : false);
        }

        private int GetItemGroupCount(Window dialogue)
        {
            Tree tree = dialogue.Get<Tree>("uxAddressTreeView");
            TreeNode root = tree.Nodes[0];
            return (int)root.Nodes.Count;
        }

       
        private void SelectGroup(int index, Window dialogue)
        {
            Tree tree = dialogue.Get<Tree>("uxAddressTreeView");
            TreeNode root = tree.Nodes[0];
            root.Nodes[index].Select();
            //GetElement(SearchCriteria.ByControlType(ControlType.TreeItem).AndIndex(index));
        }

        public void CloseGroupsDialogue(Window dialogue)
        {
            dialogue.Get<Button>("uxCloseAddressButton").Click();
        }

        public Window OpenGroupsDialogue()
        {
            manager.MainWindow.Get<Button>("groupButton").Click();
            return manager.MainWindow.ModalWindow(GROUPWINTITLE);
        }

        public List<GroupData> GetGroupList()
        {
            List<GroupData> list = new List<GroupData>();
            Window dialogue = OpenGroupsDialogue();
            Tree tree = dialogue.Get<Tree>("uxAddressTreeView");
            TreeNode root = tree.Nodes[0];
            foreach (TreeNode item in root.Nodes)
            {
                list.Add(new GroupData()
                {
                    Name = item.Text
                });
            }
            CloseGroupsDialogue(dialogue);
            return list;
        }
    }
}