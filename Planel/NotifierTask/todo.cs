using SQLite.Net.Attributes;
using System;
using System.ComponentModel;

namespace NotifierTask
{
    class todo : INotifyPropertyChanged
    {
        private int _id;
        private string _title;
        private string _detail;
        private DateTime _time;
        private byte _notify;
        private byte _isdone;
        protected void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    RaisePropertyChanged("Id");
                }
            }
        }
        
        public string title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    RaisePropertyChanged("title");
                }
            }
        }
        public string detail
        {
            get { return _detail; }
            set
            {
                if (_detail != value)
                {
                    _detail = value;
                    RaisePropertyChanged("detail");
                }
            }
        }
        public DateTime time
        {
            get { return _time; }
            set
            {
                if (_time != value)
                {
                    _time = value;
                    RaisePropertyChanged("time");
                }
            }
        }
        //0 stands for no notify, 1 stands for toast notify and 2 stands for alarm
        public byte notify
        {
            get { return _notify; }
            set
            {
                if (_notify != value)
                {
                    _notify = value;
                    RaisePropertyChanged("notify");
                }
            }
        }
        // 0 stands for undone, 1 stands for suspend(snooze) and 2 stands for done
        public byte isdone
        {
            get { return _isdone; }
            set
            {
                if (_isdone != value)
                {
                    _isdone = value;
                    RaisePropertyChanged("isdone");
                }
            }
        }

    }
}
