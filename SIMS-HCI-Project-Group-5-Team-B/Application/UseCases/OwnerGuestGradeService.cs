using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;

namespace SIMS_HCI_Project_Group_5_Team_B.Application.UseCases
{
    public class OwnerGuestGradeService
    {
        private Repository<OwnerGuestGrade> ownerGuestGradeRepository;
        private ReservationService reservationService;

        public OwnerGuestGradeService(ReservationService reservationService)
        {
            ownerGuestGradeRepository = new Repository<OwnerGuestGrade>();
            this.reservationService = reservationService;
            GetReservationReference();
        }

        public List<OwnerGuestGrade> GetAll()
        {
            return ownerGuestGradeRepository.GetAll();
        }
        public void Save(OwnerGuestGrade newOwnerGuestGrade)
        {
            ownerGuestGradeRepository.Save(newOwnerGuestGrade);
            GetReservationReference();
        }
        public void Delete(OwnerGuestGrade ownerGuestGrade)
        {
            ownerGuestGradeRepository.Delete(ownerGuestGrade);
        }
        public void Update(OwnerGuestGrade ownerGuestGrade)
        {
            ownerGuestGradeRepository.Update(ownerGuestGrade);
            GetReservationReference();
        }

        public List<OwnerGuestGrade> FindBy(string[] propertyNames, string[] values)
        {
            return ownerGuestGradeRepository.FindBy(propertyNames, values);
        }
        public OwnerGuestGrade getById(int id)
        {
            return GetAll().Find(acmd => acmd.Id == id);
        }

        private void GetReservationReference()
        {
            List<OwnerGuestGrade> ownerGuestGrades = ownerGuestGradeRepository.GetAll();
            foreach (OwnerGuestGrade ownerGuestGrade in ownerGuestGrades)
            {
                Reservation reservation = reservationService.getById(ownerGuestGrade.ReservationId);
                if (reservation != null)
                {
                    ownerGuestGrade.Reservation = reservation;
                }
            }

        }

    }

}