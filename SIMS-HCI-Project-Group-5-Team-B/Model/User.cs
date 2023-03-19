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
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if(name != value)
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                if(password != value)
                {
                    password = value;
                    OnPropertyChanged();
                }
            }
        }

        private Type type;
        public Type Type
        {
            get { return type; }
            set
            {
                if(type != value)
                {
                    type = value;
                    OnPropertyChanged();
                }
            }
        }

        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                if(id != value)
                {
                    id = value;
                    OnPropertyChanged();
                }
            }
        }

        public User(string name, string password, Type type)
        {
            this.name = name;
            this.password = password;
            this.type = type;
        }

        public User()
        {
            Name = "";
            Password = "";
            Type = Type.GUIDE;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            name = values[1];
            password = values[2];
            type = (Type)Enum.Parse(typeof(Type), values[3]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Name, Password, Type.ToString()};
            return csvValues;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
