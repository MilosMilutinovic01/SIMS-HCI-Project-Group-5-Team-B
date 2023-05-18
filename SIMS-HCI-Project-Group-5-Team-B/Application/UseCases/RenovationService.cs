using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel;
using SIMS_HCI_Project_Group_5_Team_B.Domain.ServiceInterfaces;


namespace SIMS_HCI_Project_Group_5_Team_B.Application.UseCases
{
    public class RenovationService: IRenovationService
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
            foreach (Renovation renovation in GetAll())
            {
                Accommodation accommodation = accommodationRepository.GetById(renovation.AccommodationId);
                if (accommodation != null)
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

        public List<Renovation> GetUndeleted()
        {
            return renovationRepository.GetUndeleted();
        }

        public bool IsRenovationDeletable(Renovation renovation)
        {
            //moze se obrisati,otkazati samo ako do renoviranja ima vise od 5 dana
            if (DateTime.Today.AddDays(5) < renovation.StartDate)
            {
                return true;
            }
            return false;
        }

        public List<Renovation> GetPastRenovations(int ownerId)
        {
            List<Renovation> pastRenovations = new List<Renovation>();
            foreach (Renovation renovation in GetUndeleted())
            {
                if (renovation.Accommodation.OwnerId == ownerId && renovation.StartDate < DateTime.Today)
                {
                    pastRenovations.Add(renovation);
                }
            }
            return pastRenovations;
        }


        public List<RenovationGridView> GetFutureRenovationsView(int ownerId)
        {
            List<RenovationGridView> renovationGridViews = new List<RenovationGridView>();
            foreach (Renovation renovation in GetUndeleted())
            {
                if (renovation.Accommodation.OwnerId == ownerId && renovation.StartDate >= DateTime.Today)
                {
                    bool isCancelable = true;
                    if (!IsRenovationDeletable(renovation))
                    {
                        isCancelable = false;
                    }
                    renovationGridViews.Add(new RenovationGridView(renovation, isCancelable));
                }
            }
            return renovationGridViews;

        }

        public bool IsAccomodationNotInRenovation(Accommodation selectedAccommodation, DateTime startDate, DateTime endDate)
        {
            List<Renovation> accomodationRenovations = renovationRepository.GetRenovationForAccommodation(selectedAccommodation.Id);

            foreach (Renovation renovation in accomodationRenovations)
            {
                bool isInRange = startDate >= renovation.StartDate && startDate <= renovation.EndDate ||
                                 endDate >= renovation.StartDate && endDate <= renovation.EndDate;

                bool isOutOfRange = startDate <= renovation.StartDate && endDate >= renovation.EndDate;

                if (isInRange)
                {
                    return false;
                }
                else if (isOutOfRange)
                {
                    return false;
                }
            }
            return true;

        }
    }
}
