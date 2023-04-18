using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace SIMS_HCI_Project_Group_5_Team_B.Application.UseCases
{
    public class SuperOwnerService
    {
        private OwnerAccommodationGradeSevice ownerAccommodationGradeService;
        private AccommodationService accommodationService;
        public SuperOwnerService(OwnerAccommodationGradeSevice ownerAccommodationGradeService, AccommodationService accommodationService)
        {
            this.ownerAccommodationGradeService = ownerAccommodationGradeService;
            this.accommodationService = accommodationService;
        }


        public double CalculateGradeAverage(Owner owner)
        {
            //prolazicemo kroz sve ocene i gledati koja ocenjena rezervacija pripada nasem vlasniku
            List<OwnerAccommodationGrade> ownerAccommodationGrades = ownerAccommodationGradeService.GetAll();
            int gradeNumber = 0; 
            double sum = 0;
            foreach (OwnerAccommodationGrade ownerAccommodationGrade in ownerAccommodationGrades)
            {
                if (owner.Id == ownerAccommodationGrade.Reservation.Accommodation.Owner.Id)
                {
                    sum += ownerAccommodationGrade.GradeAverage;
                    gradeNumber++;
                }
            }

            return (double)sum / gradeNumber;

        }


        public int GetNumberOfGrades(Owner owner)
        {
           
            List<OwnerAccommodationGrade> ownerAccommodationGrades = ownerAccommodationGradeService.GetAll();
            int numberOfGrades = 0;
            foreach (OwnerAccommodationGrade ownerAccommodationGrade in ownerAccommodationGrades)
            {
                if (owner.Id == ownerAccommodationGrade.Reservation.Accommodation.Owner.Id)
                {
                    numberOfGrades++;
                }
            }
            return numberOfGrades;
        }
        
        public List<Accommodation> GetSortedAccommodations()
        {
            List<Accommodation> accommodationsForSotring = accommodationService.GetAll();
            for (int i = 0; i < accommodationsForSotring.Count(); i++)
            {
                if (accommodationsForSotring[i].Owner.GradeAverage > 4.5 && GetNumberOfGrades(accommodationsForSotring[i].Owner) >= 50)
                {

                    accommodationsForSotring.Insert(0, accommodationsForSotring[i]);
                    accommodationsForSotring.RemoveAt(i + 1);

                }
            }

            return accommodationsForSotring;
        }

    }
}
