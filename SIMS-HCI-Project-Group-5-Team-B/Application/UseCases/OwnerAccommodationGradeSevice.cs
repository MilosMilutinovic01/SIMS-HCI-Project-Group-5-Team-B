using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Application.UseCases
{
    public class OwnerAccommodationGradeSevice
    {
        private Repository<OwnerAccommodationGrade> ownerAccommodationGradeRepository;
        private ReservationService reservationService;

        public OwnerAccommodationGradeSevice(ReservationService reservationService)
        {
            ownerAccommodationGradeRepository = new Repository<OwnerAccommodationGrade>();
            this.reservationService = reservationService;
            GetReservationReference();
        }

        public List<OwnerAccommodationGrade> GetAll()
        {
            return ownerAccommodationGradeRepository.GetAll();
        }
        public void Save(OwnerAccommodationGrade ownerAccommodationGrade)
        {
            ownerAccommodationGradeRepository.Save(ownerAccommodationGrade);
            GetReservationReference();
        }
        public void Delete(OwnerAccommodationGrade ownerAccommodationGrade)
        {
            ownerAccommodationGradeRepository.Delete(ownerAccommodationGrade);
        }
        public void Update(OwnerAccommodationGrade ownerAccommodationGrade)
        {
            ownerAccommodationGradeRepository.Update(ownerAccommodationGrade);
            GetReservationReference();
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
                Reservation reservation = reservationService.getById(ownerAccommodationGrade.ReservationId);
                if (reservation != null)
                {
                    ownerAccommodationGrade.Reservation = reservation;
                }
            }

        }



        public List<OwnerAccommodationGrade> GetOwnerAccommodationGradesForShowing(Owner owner)
        {
            //dobili smo sve rezervacije
            List<Reservation> reservations = reservationService.GetUndeleted();

            //dobili smo sve ocene smestaja
            List<OwnerAccommodationGrade> ownerAccommodationGrades = GetAll();

            //ocene smestaja koje ce biti prikazivane
            List<OwnerAccommodationGrade> ownerAccommodationGradesForShowing = new List<OwnerAccommodationGrade>();

            foreach (Reservation reservation in reservations)
            {
                //prolazimo kroz rezervacije i gledamo da li su je ocenili i gost i vlasnik
                if (reservation.IsGraded == true && reservation.IsGradedByGuest == true)
                {
                    //ako su ocenili i gost i  vlasnik prolazimo kroz ocene smestaja i trazimo idRezervacije koju su ocenili i gost i vlasnik
                    foreach (OwnerAccommodationGrade ownerAccommodationGrade in ownerAccommodationGrades)
                    {
                        //nasli smo ocenu smestaja sa trazenim id-om rezervacije i sada tu ocenu dodajemo u ocene koje ce se prikazati
                        if (reservation.Id == ownerAccommodationGrade.ReservationId && ownerAccommodationGrade.Reservation.Accommodation.Owner.Id == owner.Id)
                        {
                            ownerAccommodationGradesForShowing.Add(ownerAccommodationGrade);
                        }
                    }
                }
            }

            return ownerAccommodationGradesForShowing;
        }

        public double GetAverageGrade(OwnerAccommodationGrade ownerAccommodationGrade)
        {

            double averageGrade = (double)(ownerAccommodationGrade.Cleanliness + ownerAccommodationGrade.OwnerCorrectness + ownerAccommodationGrade.StateOfInventory + ownerAccommodationGrade.Privacy + ownerAccommodationGrade.Quietness) / 5;
            return averageGrade;
        }


    }
}
