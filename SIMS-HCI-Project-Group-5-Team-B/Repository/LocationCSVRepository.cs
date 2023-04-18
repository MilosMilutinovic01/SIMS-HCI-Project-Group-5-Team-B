using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Repository
{
    public class LocationCSVRepository : CSVRepository<Location>, ILocationRepository
    {
        public LocationCSVRepository() :base() { }
        public void Delete(Location location)
        {
            Location? founded = _data.Find(d => d.Id == location.Id);
            if (founded != null)
            {
                _data.Remove(founded);
                WriteCSV(_data);
            }
        }

        public List<Location> GetAll()
        {
            return _data;
        }

        public void Save(Location newLocation)
        {
            newLocation.Id = NextId();
            _data.Add(newLocation);
            WriteCSV(_data);
        }
        private int NextId()
        {
            if (_data.Count() < 1)
            {
                return 1;
            }
            else
            {
                return _data.Max(d => d.Id) + 1;
            }
        }

        public void Update(Location location)
        {
            Location? current = _data.Find(d => d.Id == location.Id);
            if (current == null)
            {
                Save(location);
                return;
            }
            int index = _data.IndexOf(current);
            _data.Remove(current);
            _data.Insert(index, location);
            WriteCSV(_data);
        }

        protected override Location FromCSV(string[] values)
        {
            Location newLocation = new Location();

            newLocation.Id = int.Parse(values[0]);
            newLocation.City = values[1];
            newLocation.State = values[2];

            return newLocation;
        }

        protected override string[] ToCSV(Location obj)
        {
            string[] csvValues =
{
                obj.Id.ToString(),
                obj.City,
                obj.State,
            };

            return csvValues;
        }
    }
}
