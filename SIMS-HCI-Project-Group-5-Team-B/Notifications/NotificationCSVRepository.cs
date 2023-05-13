using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Notifications
{
    public class NotificationCSVRepository : CSVRepository<Notification>, INotificationRepository
    {
        public NotificationCSVRepository() : base() { }

        public void Delete(Notification notification)
        {
            Notification? founded = _data.Find(d => d.Id == notification.Id);
            if (founded != null)
            {
                _data.Remove(founded);
                WriteCSV(_data);
            }
        }

        public List<Notification> GetAll()
        {
            return _data;
        }

        public void Save(Notification newNotification)
        {
            newNotification.Id = NextId();
            _data.Add(newNotification);
            WriteCSV(_data);
        }
        private int NextId()
        {
            if (_data.Count() < 1)
            {
                return 1;
            }
            else
            {
                return _data.Max(d => d.Id) + 1;
            }
        }

        public void Update(Notification notification)
        {
            Notification? current = _data.Find(d => d.Id == notification.Id);
            if (current == null)
            {
                Save(notification);
                return;
            }
            int index = _data.IndexOf(current);
            _data.Remove(current);
            _data.Insert(index, notification);
            WriteCSV(_data);
        }

        protected override Notification FromCSV(string[] values)
        {
            Notification newNotification = new Notification();

            newNotification.Id = int.Parse(values[0]);
            newNotification.ReceiverId = int.Parse(values[1]);
            newNotification.Message = values[2];
            newNotification.IsRead = Boolean.Parse(values[3]);

            return newNotification;
        }

        protected override string[] ToCSV(Notification obj)
        {
            string[] csvValues = { obj.Id.ToString(), obj.ReceiverId.ToString(), obj.Message, obj.IsRead.ToString() };
            return csvValues;
        }
    }
}
