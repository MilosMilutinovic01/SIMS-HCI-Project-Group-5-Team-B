using SIMS_HCI_Project_Group_5_Team_B.Application.Injector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Notifications
{
    public class NotificationService
    {
        private INotificationRepository notificationRepository;

        public NotificationService()
        {
            notificationRepository = Injector.CreateInstance<INotificationRepository>();
        }

        public List<Notification> GetFor(int recieverId)
        {
            return notificationRepository.GetAll().FindAll(notification => notification.ReceiverId == recieverId);
        }

        public void Delete(Notification notification)
        {
            notificationRepository.Delete(notification);
        }
    }
}
