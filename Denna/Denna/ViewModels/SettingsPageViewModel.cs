using Denna.Classes;
using System.Collections.Generic;
using System.ComponentModel;

namespace Denna.ViewModels
{
    class SettingsPageViewModel : INotifyPropertyChanged
    {
        List<Classes.ItemHolder> menuList;
        public List<Classes.ItemHolder> MenuList
        {
            get
            {
                return menuList;
            }
            set
            {
                if (menuList != value)
                {
                    menuList = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("MenuList"));
                    }
                }
            }

        }
        public SettingsPageViewModel()
        {
            adder();
        }
        void adder()
        {
            MenuList = new List<ItemHolder>();
            MenuList.Add(new ItemHolder() { icon = "", detail = "Edit profile, email", title = "Account", ID = 1 });
            MenuList.Add(new ItemHolder() { icon = "", detail = "Sounds, reminders", title = "Notifications and quick actions", ID = 3 });
            MenuList.Add(new ItemHolder() { icon = "", detail = "Color, theme", title = "Personalization", ID = 5 });
            MenuList.Add(new ItemHolder() { icon = "", detail = "Help, feedback, support, insider program", title = "Support", ID = 7 });
            MenuList.Add(new ItemHolder() { icon = "", detail = "About this app", title = "About", ID = 8 });
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}