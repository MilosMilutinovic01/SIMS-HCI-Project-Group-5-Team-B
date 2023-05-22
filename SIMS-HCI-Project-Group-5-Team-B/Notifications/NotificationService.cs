using SIMS_HCI_Project_Group_5_Team_B.Application.Injector;
using SIMS_HCI_Project_Group_5_Team_B.DTO;
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

        public List<GuideGuestNotificationDTO> GetForGuideGuest(int recieverId)
        {
            List<GuideGuestNotificationDTO> Notifications = new List<GuideGuestNotificationDTO>();
            foreach(var notification in GetFor(recieverId))
            {
                Notifications.Add(new GuideGuestNotificationDTO(notification, notification.Message.ToLower().Contains("join") ? NotificationType.TOUR_ATTENDANCE_ACCEPTION : NotificationType.NEW_TOUR));
            }
            return Notifications;
        }

        public void SendNewTourNotification(int guideGuestId, string message, int tourId)
        {
            notificationRepository.Save(new Notification(0, receiverId: guideGuestId, message, false, additionalInfo: tourId));
        }
    }
}
