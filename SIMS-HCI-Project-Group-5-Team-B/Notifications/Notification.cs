using SIMS_HCI_Project_Group_5_Team_B.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Notifications
{

    public class Notification : ISerializable, INotifyPropertyChanged
    {
        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged();
                }
            }
        }

        private int receiverId;
        public int ReceiverId
        {
            get { return receiverId; }
            set
            {
                if (receiverId != value)
                {
                    receiverId = value;
                    OnPropertyChanged();
                }
            }
        }

        private string message;
        public string Message
        {
            get { return message; }
            set
            {
                if (message != value)
                {
                    message = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool isRead;
        public bool IsRead
        {
            get { return isRead; }
            set
            {
                if(isRead != value)
                {
                    isRead = value;
                    OnPropertyChanged();
                }
            }
        }

        public Notification()
        {

        }

        public Notification(int id, int receiverId, string message, bool isRead)
        {
            Id = id;
            ReceiverId = receiverId;
            Message = message;
            IsRead = isRead;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            ReceiverId = int.Parse(values[1]);
            Message = values[2];
            IsRead = Boolean.Parse(values[3]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), ReceiverId.ToString(), Message, IsRead.ToString()};
            return csvValues;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
