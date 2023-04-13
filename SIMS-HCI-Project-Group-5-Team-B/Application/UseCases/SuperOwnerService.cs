﻿using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace SIMS_HCI_Project_Group_5_Team_B.Application.UseCases
{
    public class SuperOwnerService
    {

        private ReservationService reservationService;
        private OwnerAccommodationGradeSevice ownerAccommodationGradeService;
        private OwnerService ownerService;
        private AccommodationService accommodationService;
        public SuperOwnerService(ReservationService reservationService, OwnerAccommodationGradeSevice ownerAccommodationGradeService, OwnerService ownerService, AccommodationService accommodationService)
        {
            this.reservationService = reservationService;
            this.ownerAccommodationGradeService = ownerAccommodationGradeService;
            this.ownerService = ownerService;
            this.accommodationService = accommodationService;
        }


        public double CalculateGradeAverage(Owner owner)
        {
            //prolazicemo kroz sve ocene i gledati koja ocenjena rezervacija pripada nasem vlasniku
            List<OwnerAccommodationGrade> ownerAccommodationGrades = ownerAccommodationGradeService.GetAll();
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
            List<Reservation> reservations = reservationService.GetUndeleted(); //PROVJERITI OVO !!!!!!
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
            List<Accommodation> accommodationsForSotring = accommodationService.GetAll();
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
