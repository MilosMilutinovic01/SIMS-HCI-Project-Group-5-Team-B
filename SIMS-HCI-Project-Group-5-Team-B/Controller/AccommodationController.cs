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
        private Accommodation getById(int id)
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

    }
}
