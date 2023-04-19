using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Repository;


namespace SIMS_HCI_Project_Group_5_Team_B.Application.UseCases
{
    public class AccommodationService
    {
        private Repository<Accommodation> accomodationRepository;
        private LocationController locationController;
        private OwnerService ownerService;

        public AccommodationService(LocationController locationController, OwnerService ownerService)
        {
            accomodationRepository = new Repository<Accommodation>();
            this.locationController = locationController;
            this.ownerService = ownerService;
            GetLocationReference();
            GetOwnerReference();

        }

        public List<Accommodation> GetAll()
        {
            return accomodationRepository.GetAll();
        }
        public void Save(Accommodation newAccommodation)
        {
            accomodationRepository.Save(newAccommodation);
            GetOwnerReference();
            GetLocationReference();
        }
        public void Delete(Accommodation accommodation)
        {
            accomodationRepository.Delete(accommodation);
        }
        public void Update(Accommodation accommodation)
        {
            accomodationRepository.Update(accommodation);
            GetOwnerReference();
            GetLocationReference();
        }

        public List<Accommodation> FindBy(string[] propertyNames, string[] values)
        {
            return accomodationRepository.FindBy(propertyNames, values);
        }
        public Accommodation getById(int id)
        {
            return GetAll().Find(acmd => acmd.Id == id);
        }

        private void GetLocationReference()
        {
            List<Accommodation> accomodations = accomodationRepository.GetAll();
            foreach (Accommodation accommodation in accomodations)
            {
                Location location = locationController.getById(accommodation.LocationId);
                if (location != null)
                {
                    accommodation.Location = location;
                }
            }

        }

        private void GetOwnerReference()
        {
            List<Accommodation> accomodations = accomodationRepository.GetAll();
            foreach (Accommodation accommodation in accomodations)
            {
                Owner owner = ownerService.getById(accommodation.OwnerId);
                if (owner != null)
                {
                    accommodation.Owner = owner;
                }
            }

        }

        public List<Accommodation> GetSearchResult(int locationdId, string searchName, string type, int guestNumber = 1, int days = 100)
        {
            List<Accommodation> searchResult = new List<Accommodation>();
            searchResult.AddRange(GetAll());
            List<Accommodation> accommodations = new List<Accommodation>();
            accommodations.AddRange(searchResult);
            bool containsLocation;
            bool containsType;


            foreach (Accommodation accommodation in accommodations)
            {
                if (locationdId == -1)
                {
                    containsLocation = true; // this parameter should not impact the search if it was not included  
                }
                else
                {
                    containsLocation = accommodation.LocationId == locationdId;
                }
                if (string.IsNullOrEmpty(type))
                {
                    containsType = true;
                }
                else
                {
                    containsType = accommodation.Type == type;
                }
                bool containsName = accommodation.Name.ToLower().Contains(searchName.ToLower());
                bool minDaysInRange = accommodation.MinReservationDays <= days;
                bool maxGuestsInRange = accommodation.MaxGuests >= guestNumber;


                if (!maxGuestsInRange || !minDaysInRange || !containsName || !containsLocation || !containsType)
                {

                    searchResult.Remove(accommodation);
                }
            }

            return searchResult;
        }

       
        public List<Accommodation> GetAccommodationsOfLogedInOwner(Owner owner)
        {
            List<Accommodation> accomodations = accomodationRepository.GetAll();
            List<Accommodation> accommodationsOfLogedInOwner = new List<Accommodation>();
            foreach(Accommodation accommodation in accomodations)
            {
                if(accommodation.Owner.Id == owner.Id)
                {
                    accommodationsOfLogedInOwner.Add(accommodation);
                }
            }

            return accommodationsOfLogedInOwner;
        }

    }
}
