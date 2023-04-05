using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using SIMS_HCI_Project_Group_5_Team_B.Model;
namespace SIMS_HCI_Project_Group_5_Team_B.Controller
{
    public class OwnerGuestGradeContoller
    {
        private Repository<OwnerGuestGrade> ownerGuestGradeRepository;
        private ReservationController reservationController;

        public OwnerGuestGradeContoller(ReservationController reservationController)
        {
            ownerGuestGradeRepository = new Repository<OwnerGuestGrade>();
            this.reservationController = reservationController;
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
                Reservation reservation = reservationController.getById(ownerGuestGrade.ReservationId);
                if (reservation != null)
                {
                    ownerGuestGrade.Reservation = reservation;
                }
            }

        }





    }

}