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
    public class OwnerNotificationsViewModel
    {
        public List<Notification> Notifications { get; set; }
        private NotificationController notificationController;
        private UserController userController;
        private OwnerService ownerService;

        public OwnerNotificationsViewModel(int ownerId)
        {
            notificationController = new NotificationController();
            userController = new UserController();
            ownerService = new OwnerService();
            Owner owner = ownerService.getById(ownerId);
            int userId = userController.GetByUsername(owner.Username).Id;
            Notifications = new List<Notification>(notificationController.Get(userId));
        }


    }
}
