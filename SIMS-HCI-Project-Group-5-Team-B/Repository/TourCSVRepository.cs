using SIMS_HCI_Project_Group_5_Team_B.Application.Injector;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace SIMS_HCI_Project_Group_5_Team_B.Repository
{
    public class TourCSVRepository : CSVRepository<Tour>, ITourRepository
    {
        public bool LoadedData = false;
        public TourCSVRepository() : base() { }

        public void LoadData()
        {
            LoadLocation();
            LoadKeypoints();
        }

        private void LoadLocation()
        {
            ILocationRepository locationRepository = Injector.CreateInstance<ILocationRepository>();
            foreach (var tour in _data)
            {
                tour.Location = locationRepository.GetAll().Find(l => l.Id == tour.LocationId);
            }
        }

        private void LoadKeypoints()
        {
            IKeyPointRepository keyPointRepository = Injector.CreateInstance<IKeyPointRepository>();
            foreach (var tour in _data)
            {
                tour.KeyPoints = keyPointRepository.GetAll().FindAll(kp => kp.TourId == tour.Id);
            }
        }

        public void Delete(Tour tour)
        {
            Tour? founded = _data.Find(d => d.Id == tour.Id);
            if (founded != null)
            {
                _data.Remove(founded);
                WriteCSV(_data);
            }
        }

        public List<Tour> GetAll()
        {
            return _data;
        }

        public void Save(Tour newTour)
        {
            newTour.Id = NextId();
            _data.Add(newTour);
            WriteCSV(_data);
        }
        public int NextId()
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

        public void Update(Tour tour)
        {
            Tour? current = _data.Find(d => d.Id == tour.Id);
            if (current == null)
            {
                Save(tour);
                return;
            }
            int index = _data.IndexOf(current);
            _data.Remove(current);
            _data.Insert(index, tour);
            WriteCSV(_data);
        }

        protected override Tour FromCSV(string[] values)
        {
            Tour newTour = new Tour();

            newTour.Id = int.Parse(values[0]);
            newTour.Name = values[1];
            newTour.LocationId = int.Parse(values[2]);
            newTour.Description = values[3];
            newTour.Language = values[4];
            newTour.MaxGuests = int.Parse(values[5]);
            string[] parts = values[6].Split(',');
            for (int i = 0; i < parts.Length; i++)
            {
                newTour.KeyPoints.Add(new KeyPoint(parts[i]));
            }
            newTour.Duration = double.Parse(values[7]);
            newTour.ImageUrls = values[8];

            return newTour;
        }

        protected override string[] ToCSV(Tour obj)
        {
            string stringBuilder = "";
            foreach (KeyPoint kp in obj.KeyPoints)
            {
                stringBuilder += kp.Name + ",";
            }
            stringBuilder = stringBuilder.Substring(0, stringBuilder.Length - 1);
            string[] csvValues =
            {
                obj.Id.ToString(),
                obj.Name,
                obj.LocationId.ToString(),
                obj.Description,
                obj.Language,
                obj.MaxGuests.ToString(),
                stringBuilder,
                obj.Duration.ToString(),
                obj.ImageUrls
            };
            return csvValues;
        }
    }
}
