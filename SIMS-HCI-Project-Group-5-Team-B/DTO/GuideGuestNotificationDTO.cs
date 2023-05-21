using SIMS_HCI_Project_Group_5_Team_B.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.DTO
{
    public enum NotificationType { TOUR_ATTENDANCE_ACCEPTION, NEW_TOUR }
    public class GuideGuestNotificationDTO
    {
        public Notification Notification { get; set; }
        public NotificationType Type { get; set; }

        public GuideGuestNotificationDTO(Notification notification, NotificationType notificationType)
        {
            Notification = new Notification();
            Notification.Id = notification.Id;
            Notification.ReceiverId = notification.ReceiverId;
            Notification.Message = notification.Message;
            Notification.IsRead = notification.IsRead;
            Type = notificationType;
        }
    }
}
