using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using SIMS_HCI_Project_Group_5_Team_B.Repository;


namespace SIMS_HCI_Project_Group_5_Team_B.Application.UseCases
{
    public class AccommodationService
    {
        private IAccommodationRepository accomodationRepository;
        private LocationController locationController;
        private OwnerService ownerService;

        public AccommodationService(LocationController locationController, OwnerService ownerService)
        {
            accomodationRepository = Injector.Injector.CreateInstance<IAccommodationRepository>();
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
        public Accommodation GetById(int id)
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
            searchResult.AddRange(GetUndeleted());
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

       
        public List<Accommodation> GetAccommodationsOfLogedInOwner(int ownerId)
        {
            List<Accommodation> accomodations = accomodationRepository.GetUndeleted();
            List<Accommodation> accommodationsOfLogedInOwner = new List<Accommodation>();
            foreach(Accommodation accommodation in accomodations)
            {
                if(accommodation.Owner.Id == ownerId)
                {
                    accommodationsOfLogedInOwner.Add(accommodation);
                }
            }

            return accommodationsOfLogedInOwner;
        }

        public List<string> GetOwnersAccommodations(Owner targetOwner)
        {
            List<Accommodation> allAccommodations = GetUndeleted();
            List<string> matchingAccommodationNames = allAccommodations
                .Where(accommodation => accommodation.OwnerId == targetOwner.Id)
                .Select(accommodation => accommodation.Name)
                .Distinct()
                .ToList();

            return matchingAccommodationNames;
        }

        public int GetIdByName(string name, Owner owner)
        {
            foreach(Accommodation accommodation in GetUndeleted())
            {
                if(accommodation.Name == name && accommodation.OwnerId == owner.Id)
                {
                    return accommodation.Id;
                }
            }
            return -1;
        }

        public bool DoesOwnerHaveAccommodationOnLocation(Owner owner,Location location)
        {
            foreach(Accommodation accommodation in GetUndeleted()) //idemo kroz smestaje ako naidjemo na prvi smestaj zeljenog vlasnika na zeljenoj lokaciji onda je true
            {
                if(accommodation.Owner.Id == owner.Id && accommodation.Location.State == location.State && accommodation.Location.City == location.City)
                {
                    return true;
                }
            }
            return false;//u suprotnom false
        }


        public List<Accommodation> GetOwnersAccommodationsOnLocation(Location location,Owner owner)
        {
            List<Accommodation> accommodationsOnLocation = new List<Accommodation>();
            //ovde ce trebati getUndeleted kad krenem da zatvraam smestaje
            foreach (Accommodation accommodation in GetUndeleted())
            {
                if (accommodation.LocationId == location.Id && accommodation.Owner.Id == owner.Id)
                {
                    accommodationsOnLocation.Add(accommodation);
                }
            }
            return accommodationsOnLocation;
        }

        public List<Location> GetLocationsWithAccommodation()
        {
            List<Location> locationsWithAccommodation = new List<Location>();
            //ovde ce trebati getUndeleted kad krenem da zatvraam smestaje
            foreach (Accommodation accommodation in GetUndeleted())
            {
                locationsWithAccommodation.Add(accommodation.Location);
            }
            List<Location> distinctLocations = locationsWithAccommodation.Distinct().ToList();
            return distinctLocations;
        }

        public List<Accommodation> GetAccommodationsOnLocation(Location location)
        {
            List<Accommodation> accommodationsOnLocation = new List<Accommodation>();
            //ovde ce trebati getUndeleted kad krenem da zatvraam smestaje
            foreach(Accommodation accommodation in GetUndeleted())
            {
                if(accommodation.LocationId == location.Id)
                {
                    accommodationsOnLocation.Add(accommodation);
                }
            }
            return accommodationsOnLocation;
        }

        public List<Accommodation> GetUndeleted()
        {
            return accomodationRepository.GetUndeleted();
        }

        public List<Accommodation> GetOwnersAccommodationsOnUnpopularLocations(List<Location> unpopularLocations, Owner owner)
        {
            List<Accommodation> accommodations = new List<Accommodation>();
            foreach (Location location in unpopularLocations)
            {
                foreach (Accommodation accommodation in GetOwnersAccommodationsOnLocation(location, owner))
                {
                    accommodations.Add(accommodation);
                }
            }
            return accommodations;
        }

    }
}
