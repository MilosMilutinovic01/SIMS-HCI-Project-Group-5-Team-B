using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_HCI_Project_Group_5_Team_B.Model;
using SIMS_HCI_Project_Group_5_Team_B.Repository;

namespace SIMS_HCI_Project_Group_5_Team_B.Controller
{
    public class SuperOwnerController
    {

        private ReservationController reservationController;
        private OwnerAccommodationGradeController ownerAccommodationGradeController;
        private OwnerController ownerController;
        private AccommodationController accommodationController;
        public SuperOwnerController(ReservationController reservationController, OwnerAccommodationGradeController ownerAccommodationGradeController, OwnerController ownerController, AccommodationController accommodationController)
        {
            this.reservationController = reservationController;
            this.ownerAccommodationGradeController = ownerAccommodationGradeController;
            this.ownerController = ownerController;
            this.accommodationController = accommodationController;
        }


        public double CalculateGradeAverage(Owner owner)
        {
            //prolazicemo kroz sve ocene i gledati koja ocenjena rezervacija pripada nasem vlasniku
            List<OwnerAccommodationGrade> ownerAccommodationGrades = ownerAccommodationGradeController.GetAll();
            int i = 0; //brojac ocena
            double sum = 0;
            foreach (OwnerAccommodationGrade ownerAccommodationGrade in ownerAccommodationGrades)
            {
                if (owner.Id == ownerAccommodationGrade.Reservation.Accommodation.Owner.Id)
                {
                    sum += ownerAccommodationGrade.GradeAverage;
                    i++;
                }
            }

            return (double)sum / i;

        }


        public int GetNumberOfReservations(Owner owner)
        {
            List<Reservation> reservations = reservationController.GetAll();
            int numberOfReservations = 0;
            foreach (Reservation reservation in reservations)
            {
                if (owner.Id == reservation.Accommodation.Owner.Id)
                {
                    numberOfReservations++;
                }
            }

            return numberOfReservations;
        }

        public List<Accommodation> AccommodationsForShowing()
        {
            List<Accommodation> accommodationsForSotring = accommodationController.GetAll();
            for (int i = 0; i < accommodationsForSotring.Count(); i++)
            {
                if (accommodationsForSotring[i].Owner.GradeAverage > 4.5 && GetNumberOfReservations(accommodationsForSotring[i].Owner) >= 50)
                {

                    accommodationsForSotring.Insert(0, accommodationsForSotring[i]);
                    accommodationsForSotring.RemoveAt(i + 1);

                }
            }

            return accommodationsForSotring;
        }

    }
}
