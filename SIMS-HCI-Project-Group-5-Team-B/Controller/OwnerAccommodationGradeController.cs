using SIMS_HCI_Project_Group_5_Team_B.Model;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Controller
{
    public class OwnerAccommodationGradeController
    {
        private Repository<OwnerAccommodationGrade> ownerAccommodationGradeRepository;
        private ReservationController reservationController;

        public OwnerAccommodationGradeController(ReservationController reservationController)
        {
            ownerAccommodationGradeRepository = new Repository<OwnerAccommodationGrade>();
            this.reservationController = reservationController;
            GetReservationReference();
        }

        public List<OwnerAccommodationGrade> GetAll()
        {
            return ownerAccommodationGradeRepository.GetAll();
        }
        public void Save(OwnerAccommodationGrade ownerAccommodationGrade)
        {
            ownerAccommodationGradeRepository.Save(ownerAccommodationGrade);
        }
        public void Delete(OwnerAccommodationGrade ownerAccommodationGrade)
        {
            ownerAccommodationGradeRepository.Delete(ownerAccommodationGrade);
        }
        public void Update(OwnerAccommodationGrade ownerAccommodationGrade)
        {
            ownerAccommodationGradeRepository.Update(ownerAccommodationGrade);
        }

        public List<OwnerAccommodationGrade> FindBy(string[] propertyNames, string[] values)
        {
            return ownerAccommodationGradeRepository.FindBy(propertyNames, values);
        }
        public OwnerAccommodationGrade getById(int id)
        {
            return GetAll().Find(acmd => acmd.Id == id);
        }

        private void GetReservationReference()
        {
            List<OwnerAccommodationGrade> ownerAccommodationGrades = ownerAccommodationGradeRepository.GetAll();
            foreach (OwnerAccommodationGrade ownerAccommodationGrade in ownerAccommodationGrades)
            {
                Reservation reservation = reservationController.getById(ownerAccommodationGrade.ReservationId);
                if (reservation != null)
                {
                    ownerAccommodationGrade.Reservation = reservation;
                }
            }

        }
    }
}
