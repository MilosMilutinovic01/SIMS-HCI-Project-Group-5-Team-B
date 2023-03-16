using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_HCI_Project_Group_5_Team_B.Model;
using SIMS_HCI_Project_Group_5_Team_B.Repository;

namespace SIMS_HCI_Project_Group_5_Team_B.Controller
{
    public class AccommodationController
    {
        private Repository<Accommodation> accomodationRepository;
        private LocationController locationController;

        public AccommodationController(LocationController locationController)
        {
            accomodationRepository = new Repository<Accommodation>();
            this.locationController = locationController;
            GetLocationReference();
        }

        public List<Accommodation> GetAll()
        {
            return accomodationRepository.GetAll();
        }
        public void Save(Accommodation newAccommodation)
        {
            accomodationRepository.Save(newAccommodation);
        }
        public void Delete(Accommodation accommodation)
        {
            accomodationRepository.Delete(accommodation);
        }
        public void Update(Accommodation accommodation)
        {
            accomodationRepository.Update(accommodation);
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
            foreach(Accommodation accommodation in accomodations)
            {
                Location location = locationController.getById(accommodation.LocationId);
                if (location != null)
                {
                    accommodation.Location = location;
                }
            }

        }

        public void AddAccommodation(Accommodation accommodation, Location location)
        {
            List<Location> savedLocations = locationController.GetAll();


            if (savedLocations.Count() != 0)
            {
                for (int i = 0; i < savedLocations.Count(); i++)
                {
                    if (savedLocations[i].City == location.City && savedLocations[i].State == location.State)
                    {
                        //location already exists in data
                        accommodation.LocationId = savedLocations[i].Id;
                        accomodationRepository.Save(accommodation);
                        break;
                    }
                    else if (i == savedLocations.Count() - 1)
                    {
                        locationController.Save(location);
                        accommodation.LocationId = location.Id;
                        accomodationRepository.Save(accommodation);
                    }
                }
            }
            else
            {
                locationController.Save(location);
                accommodation.LocationId = location.Id;
                accomodationRepository.Save(accommodation);
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
                if(string.IsNullOrEmpty(type))
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


                if (!maxGuestsInRange || !minDaysInRange || !containsName || !containsLocation || !containsType )
                {
                    
                    searchResult.Remove(accommodation);
                }
            }

            return searchResult;
        }


    }
}
