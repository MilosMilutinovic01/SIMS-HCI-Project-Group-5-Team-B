using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Notifications;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class OwnerGuestNotificationsViewModel
    {
        public List<Notification> Notifications { get; set; }
        private NotificationController notificationController;
        private UserController userController;
        private OwnerGuestService ownerGuestService;

        public OwnerGuestNotificationsViewModel(int ownerGuestId) 
        {
            notificationController = new NotificationController();
            userController = new UserController();
            ownerGuestService = new OwnerGuestService();
            OwnerGuest ownerGuest = ownerGuestService.GetById(ownerGuestId);
            int userId = userController.GetByUsername(ownerGuest.Username).Id;
            Notifications = new List<Notification>(notificationController.Get(userId));
        } 

    }
}
