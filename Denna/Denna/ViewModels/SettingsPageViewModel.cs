using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Denna.Classes;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Denna.ViewModels
{
    class SettingsPageViewModel : INotifyPropertyChanged

    {
        private List<Classes.ItemHolder> _MenuList;
        public List<Classes.ItemHolder> MenuList
        {
            get
            {

                return _MenuList;

            }
            set
            {
                if (_MenuList != value)
                {
                    _MenuList = value;
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
        private void adder()
        {
            MenuList = new List<ItemHolder>();
            MenuList.Add(new ItemHolder() { icon = "", detail = "Edit profile, email", title = "Account", ID = 1 });
            MenuList.Add(new ItemHolder() { icon = "", detail = "Connected devices, sessions", title = "Privacy", ID = 2 });
            MenuList.Add(new ItemHolder() { icon = "", detail = "Sounds, reminders", title = "Notifications", ID = 3 });
            MenuList.Add(new ItemHolder() { icon = "", detail = "Action center tools", title = "Quick Actions", ID = 4 });
            MenuList.Add(new ItemHolder() { icon = "", detail = "Color, theme", title = "Personalization", ID = 5 });
            MenuList.Add(new ItemHolder() { icon = "", detail = "App language", title = "Localization", ID = 6 });
            MenuList.Add(new ItemHolder() { icon = "", detail = "Help, feedback, support, insider, translation", title = "Support", ID = 7 });
            MenuList.Add(new ItemHolder() { icon = "", detail = "About developers", title = "About", ID = 8 });
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }

}
