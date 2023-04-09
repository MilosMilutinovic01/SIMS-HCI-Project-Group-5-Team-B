using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Application.UseCases
{
    public class ReservationChangeRequestService
    {
         
        private IReservationChangeRequestRepository reservationChangeRequestRepository;
        private IReservationRepository reservationRepository;
        public ReservationChangeRequestService(IReservationChangeRequestRepository reservationChangeRequestRepository, IReservationRepository reservationRepository) 
        { 
            this.reservationChangeRequestRepository = reservationChangeRequestRepository;
            this.reservationRepository = reservationRepository;
            GetReservationReference();
        } 
        
        public List<ReservationChangeRequest> GetAll() 
        { 
            return reservationChangeRequestRepository.GetAll();
        }
        
        public ReservationChangeRequest GetById(int id)
        {
            return reservationChangeRequestRepository.GetById(id);
        }

        public void Save(ReservationChangeRequest reservationChangeRequest)
        {
            reservationChangeRequestRepository.Save(reservationChangeRequest);
            GetReservationReference();
        }

        public void Update(ReservationChangeRequest reservationChangeRequest)
        {
            reservationChangeRequestRepository.Update(reservationChangeRequest);
            GetReservationReference();
        }

        private void GetReservationReference()
        {
            foreach(ReservationChangeRequest request in GetAll())
            {
                Reservation reservation = reservationRepository.GetById(request.ReservationId);
                if(reservation != null)
                {
                    request.Reservation = reservation;
                }
            }
        }
    }
}
