using SIMS_HCI_Project_Group_5_Team_B.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Model
{
    public enum Type { GUIDE = 0, GUIDEGUEST = 1, OWNER = 2, OWNERGUEST = 3}
    public class User : ISerializable, INotifyPropertyChanged
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if(_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                if(_password != value)
                {
                    _password = value;
                    OnPropertyChanged();
                }
            }
        }

        private Type _type;
        public Type Type
        {
            get { return _type; }
            set
            {
                if(_type != value)
                {
                    _type = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                if(_id != value)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }

        public User(string Name, string Password, Type Type)
        {
            this.Name = Name;
            this.Password = Password;
            this.Type = Type;
        }

        public User()
        {
            Name = "";
            Password = "";
            Type = Type.GUIDEGUEST;
        }

        public void FromCSV(string[] values)
        {
            Name = values[0];
            Password = values[1];
            Type = (Type)Enum.Parse(typeof(Type), values[2]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Name, Password, Type.ToString()};
            return csvValues;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
