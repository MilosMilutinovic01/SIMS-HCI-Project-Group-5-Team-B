using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces
{
    // dodati metode, postepeno
    public interface IReservationChangeRequestRepository
    {
        public List<ReservationChangeRequest> GetAll();
        public ReservationChangeRequest GetById(int id);
        public void Save(ReservationChangeRequest reservationChangeRequest);
        public void Delete(ReservationChangeRequest reservationChangeRequest);
        public void Update(ReservationChangeRequest reservationChangeRequest);
        

    }
}
