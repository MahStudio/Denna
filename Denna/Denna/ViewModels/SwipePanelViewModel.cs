using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Core.Domain;
using Core.Todos.Tasks;
using PubSub;
using Realms;

namespace Denna.ViewModels
{
    class SwipePanelViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _greet;
        private string _todate = DateTime.Now.ToLocalTime().ToString("D");
        public SwipePanelViewModel()
        {
            this.Subscribe<Classes.Header>(Text =>
            {
                Greet = Text.Text;
            });
            ToDos = TodoService.GetMustDoList();

        }
        public IRealmCollection<Todo> ToDos { get; set; }

        public string Greet
        {
            get
            {

                return _greet;

            }
            set
            {
                if (_greet != value)
                {
                    _greet = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("Greet"));
                    }
                }

            }
        }
        public string Todate
        {
            get
            {

                return _todate;

            }
            set
            {
                if (_todate != value)
                {
                    _todate = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("Todate"));
                    }
                }

            }
        }
    }
}
