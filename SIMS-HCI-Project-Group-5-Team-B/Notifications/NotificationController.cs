using SIMS_HCI_Project_Group_5_Team_B.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Notifications
{
    public class NotificationController
    {
        private Repository<Notification> notificationRepository;


        public NotificationController()
        {
            notificationRepository = new Repository<Notification>();
        }

        public void Send(Notification notification)
        {
            notificationRepository.Save(notification);
        }

        public List<Notification> Get(int receiverId)
        {
            List<Notification> result = new List<Notification>();

            foreach(var notification in notificationRepository.GetAll())
            {
                if(notification.ReceiverId == receiverId) result.Add(notification);
            }

            foreach (var notification in result)
            {
                notificationRepository.Delete(notification);
            }
            return result;
        }

        public void Delete(Notification searchedNotification)
        {
            foreach(var notification in notificationRepository.GetAll())
            {
                if(notification.Id == searchedNotification.Id)
                {
                    notificationRepository.Delete(notification);
                    return;
                }
            }
        }

        public bool Exists(int receiverId)
        {
            return notificationRepository.GetAll().Any(ntf => ntf.ReceiverId == receiverId);
        }
    }
}
