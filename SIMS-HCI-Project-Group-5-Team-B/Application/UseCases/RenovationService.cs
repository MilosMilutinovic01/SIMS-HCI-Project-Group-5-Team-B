using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;


namespace SIMS_HCI_Project_Group_5_Team_B.Application.UseCases
{
    public class RenovationService
    {
        private IRenovationRepository renovationRepository;
        private IAccommodationRepository accommodationRepository;

        public RenovationService()
        {
            this.renovationRepository = Injector.Injector.CreateInstance<IRenovationRepository>();
            this.accommodationRepository = Injector.Injector.CreateInstance<IAccommodationRepository>();
            GetAccommodationReference();
        }

        public List<Renovation> GetAll()
        {
            return renovationRepository.GetAll();
        }

        public Renovation GetById(int id)
        {
            return renovationRepository.GetById(id);
        }

        public void Save(Renovation renovation)
        {
            renovationRepository.Save(renovation);
            GetAccommodationReference();
        }

        public void Update(Renovation renovation)
        {
            renovationRepository.Update(renovation);
            GetAccommodationReference();
        }

        private void GetAccommodationReference()
        {
            foreach(Renovation renovation in GetAll())
            {
                Accommodation accommodation = accommodationRepository.GetById(renovation.AccommodationId);
                if(accommodation != null)
                {
                    renovation.Accommodation = accommodation;
                }
            }
        }

        public void MarkRenovatiosThatTookPlaceInTheLastYear()
        {
            foreach (Accommodation accommodation in accommodationRepository.GetAll())
            {
                bool renovatedInLastYear = false;
                foreach (Renovation renovation in renovationRepository.GetRenovationForAccommodation(accommodation.Id))
                {
                    if (renovation.EndDate.AddYears(1) >= DateTime.Today && renovation.EndDate < DateTime.Today)
                    {
                        renovatedInLastYear = true;
                        break;
                    }
                }
                accommodation.IsRenovatedInTheLastYear = renovatedInLastYear;
                accommodationRepository.Update(accommodation);
            }
        }

    }
}
