using Core.Domain;
using Core.Todos.Tasks;
using PubSub;
using Realms;
using System;
using System.ComponentModel;

namespace Denna.ViewModels
{
    class SwipePanelViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        string greet;
        string todate = DateTime.Now.ToLocalTime().ToString("D");
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
                return greet;
            }
            set
            {
                if (greet != value)
                {
                    greet = value;
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
                return todate;
            }
            set
            {
                if (todate != value)
                {
                    todate = value;
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