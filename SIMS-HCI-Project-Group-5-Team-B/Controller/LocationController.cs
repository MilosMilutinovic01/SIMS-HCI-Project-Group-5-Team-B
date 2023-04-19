using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using System.IO;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;

namespace SIMS_HCI_Project_Group_5_Team_B.Controller
{
    public class LocationController
    {
        private Repository<Location> locationRepository;

        public LocationController()
        {
            locationRepository = new Repository<Location>();
        }

        public List<Location> GetAll()
        {
            return locationRepository.GetAll();
        }
        public void Save(Location newLocation)
        {
            locationRepository.Save(newLocation);
        }
        public void Delete(Location location)
        {
            locationRepository.Delete(location);
        }
        public void Update(Location location)
        {
            locationRepository.Update(location);
        }

        public List<Location> FindBy(string[] propertyNames, string[] values)
        {
            return locationRepository.FindBy(propertyNames, values);
        }

        public Location getById(int id)
        {
            return GetAll().Find(loc => loc.Id == id);
        }
        public int makeId()
        {
            return locationRepository.NextId();
        }

        public List<string> GetStates()
        {
            return locationRepository.GetAll().Select(location => location.State).Distinct().ToList();

        }

        public Location GetLocation(Location location)
        {
            return GetAll().Find(l => l.City == location.City && l.State == location.State);
        }

        public List<string> GetCityByState(string state)
        {
            return locationRepository.GetAll().Where(location => location.State.Equals(state)).Select(location => location.City).Distinct().ToList();
        }
    }
}
