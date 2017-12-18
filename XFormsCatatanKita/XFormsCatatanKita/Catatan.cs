using System;
using System.Collections.Generic;
using System.Text;

using SQLite;
using System.ComponentModel;

namespace XFormsCatatanKita
{
    [Table("Catatans")]
    public class Catatan : INotifyPropertyChanged
    {
        private int _id;
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                this._id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private string _name;
        [NotNull]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                this._name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _title;
        [NotNull]
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                this._title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        private string _content;
        [NotNull]
        public string Content
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
                OnPropertyChanged(nameof(Content));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
