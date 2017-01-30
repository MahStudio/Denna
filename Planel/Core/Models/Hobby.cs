using Newtonsoft.Json;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Hobby : INotifyPropertyChanged
    {
        private int _id;
        private string _title;
        private string _detail;
        private TimeSpan _time;
        private byte _notify;
        private string _weekdays;
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
        [JsonProperty("0")]
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
        [JsonProperty("1")]
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
        [JsonProperty("2")]
        public TimeSpan time
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
        [JsonProperty("3")]
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
        [JsonProperty("4")]
        // 0 stands for undone, 1 stands for suspend(snooze) and 2 stands for done
        public string Days
        {
            get { return _weekdays; }
            set
            {
                if (_weekdays != value)
                {
                    _weekdays = value;
                    RaisePropertyChanged("Days");
                }
            }
        }
    }
   
}
