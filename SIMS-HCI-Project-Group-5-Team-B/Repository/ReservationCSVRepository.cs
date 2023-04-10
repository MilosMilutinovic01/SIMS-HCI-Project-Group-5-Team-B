using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Repository
{
    public class ReservationCSVRepository : IReservationRepository
    {
        Repository<Reservation> reservationRepository;

        public ReservationCSVRepository()
        {
            reservationRepository = new Repository<Reservation>();
        }


        public void Delete(Reservation reservation)
        {
            reservationRepository.Delete(reservation);
        }

        public List<Reservation> FindBy(string[] propertyNames, string[] values)
        {
            return reservationRepository.FindBy(propertyNames, values);
        }

        public List<Reservation> GetUndeleted()
        {
            List<Reservation> undeletedReservations = new List<Reservation>();
            foreach(Reservation reservation in reservationRepository.GetAll())
            {
                if(!reservation.IsDeleted)
                    undeletedReservations.Add(reservation);
            }
            return undeletedReservations;
        }

        public Reservation GetById(int id)
        {
            return reservationRepository.FindId(id);
        }

        public void Save(Reservation reservation)
        {
            reservationRepository.Save(reservation);
        }

        public void SaveAll(List<Reservation> reservations)
        {
            reservationRepository.SaveAll(reservations);
        }

        public void Update(Reservation reservation)
        {
            reservationRepository.Update(reservation);
        }

        public List<Reservation> GetAll()
        {
            return reservationRepository.GetAll();
        }
    }
}
