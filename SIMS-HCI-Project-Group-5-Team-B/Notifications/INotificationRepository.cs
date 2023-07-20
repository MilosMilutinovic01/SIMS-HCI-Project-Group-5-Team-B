using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Notifications
{
    public interface INotificationRepository
    {
        public void Save(Notification newNotification);

        public void Delete(Notification notification);

        public void Update(Notification notification);

        public List<Notification> GetAll();
    }
}
