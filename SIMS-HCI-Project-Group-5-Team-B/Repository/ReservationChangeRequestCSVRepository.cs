using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Repository
{
    // dodati da implementira interfejs
    public class ReservationChangeRequestCSVRepository : IReservationChangeRequestRepository
    {
        private readonly Repository<ReservationChangeRequest> reservationChangeRequestRepo;

        public ReservationChangeRequestCSVRepository()
        {
            this.reservationChangeRequestRepo = new Repository<ReservationChangeRequest>();
        }   

        public void Delete(ReservationChangeRequest reservationChangeRequest)
        {
            reservationChangeRequestRepo.Delete(reservationChangeRequest);
        }

        public List<ReservationChangeRequest> GetAll()
        {
           return reservationChangeRequestRepo.GetAll();
        }

        public ReservationChangeRequest GetById(int id)
        {
            return reservationChangeRequestRepo.FindId(id);
        }

        public void Save(ReservationChangeRequest reservationChangeRequest)
        {
            reservationChangeRequestRepo.Save(reservationChangeRequest);
        }

        public void Update(ReservationChangeRequest reservationChangeRequest)
        {
            reservationChangeRequestRepo.Update(reservationChangeRequest);
        }
    }
}
