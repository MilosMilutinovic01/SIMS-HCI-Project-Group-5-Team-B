using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Application.UseCases
{
    public class LocationService
    {
        private ILocationRepository locationRepository;

        public LocationService(ILocationRepository locationRepository)
        {
            this.locationRepository = locationRepository;
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

        public Location getById(int id)
        {
            return locationRepository.GetAll().Find(loc => loc.Id == id);
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
