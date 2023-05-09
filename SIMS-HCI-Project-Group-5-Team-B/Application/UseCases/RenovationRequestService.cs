using SIMS_HCI_Project_Group_5_Team_B.Application.Injector;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Application.UseCases
{
    public class RenovationRequestService
    {
        private IRenovationRequestRepository renovationRequestRepository;
        private IReservationRepository reservationRepository;
        public RenovationRequestService()
        {
            renovationRequestRepository = Injector.Injector.CreateInstance<IRenovationRequestRepository>();
            reservationRepository = Injector.Injector.CreateInstance<IReservationRepository>();
            GetReservationReferences();
        }

        public void Delete(RenovationRequest renovationRequest)
        {
            renovationRequestRepository.Delete(renovationRequest);
            GetReservationReferences();
        }

        public List<RenovationRequest> GetAll()
        {
            return renovationRequestRepository.GetAll();
        }

        public RenovationRequest GetById(int id)
        {
            return renovationRequestRepository.GetById(id);
        }

        public void Save(RenovationRequest renovationRequest)
        {
            renovationRequestRepository.Save(renovationRequest);
            GetReservationReferences();
        }

        public void Update(RenovationRequest renovationRequest)
        {
            renovationRequestRepository.Update(renovationRequest);
            GetReservationReferences();
        }

        private void GetReservationReferences()
        {
            foreach(RenovationRequest renovationRequest in GetAll())
            {
                Reservation reservation = reservationRepository.GetById(renovationRequest.ReservationId);
                if(reservation != null)
                {
                    renovationRequest.Reservation = reservation;
                }
            }
        }



    }
}
