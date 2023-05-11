using SIMS_HCI_Project_Group_5_Team_B.Application.Injector;
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIMS_HCI_Project_Group_5_Team_B.Repository
{
    public class TourRequestCSVRepository : CSVRepository<TourRequest>, ITourRequestRepository
    {
        public TourRequestCSVRepository() : base() { }

        public void LoadData()
        {
            ILocationRepository locationRepository = Injector.CreateInstance<ILocationRepository>();
            foreach(var request in _data)
            {
                request.Location = locationRepository.GetAll().Find(l => l.Id == request.LocationId);
            }
        }

        public void Delete(TourRequest tourRequest)
        {
            TourRequest? founded = _data.Find(d => d.Id == tourRequest.Id);
            if (founded != null)
            {
                _data.Remove(founded);
                WriteCSV(_data);
            }
        }

        public List<TourRequest> GetAll()
        {
            return _data;
        }

        public void Save(TourRequest newTourRequest)
        {
            newTourRequest.Id = NextId();
            _data.Add(newTourRequest);
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

        public void Update(TourRequest tourRequest)
        {
            TourRequest? current = _data.Find(d => d.Id == tourRequest.Id);
            if (current == null)
            {
                Save(tourRequest);
                return;
            }
            int index = _data.IndexOf(current);
            _data.Remove(current);
            _data.Insert(index, tourRequest);
            WriteCSV(_data);
        }

        protected override TourRequest FromCSV(string[] values)
        {
            TourRequest newTourRequest = new TourRequest();

            newTourRequest.Id = int.Parse(values[0]);
            newTourRequest.LocationId = int.Parse(values[1]);
            newTourRequest.Description = values[2];
            newTourRequest.Language = values[3];
            newTourRequest.MaxGuests = int.Parse(values[4]);
            newTourRequest.DateRangeStart = DateOnly.Parse(values[5]);
            newTourRequest.DateRangeEnd = DateOnly.Parse(values[6]);
            newTourRequest.Status = values[7];

            return newTourRequest;
        }

        protected override string[] ToCSV(TourRequest obj)
        {
            string[] csvValues =
            {
                obj.Id.ToString(),
                obj.LocationId.ToString(),
                obj.Description,
                obj.Language,
                obj.MaxGuests.ToString(),
                obj.DateRangeStart.ToString(),
                obj.DateRangeEnd.ToString(),
                obj.Status,
            };
            return csvValues;
        }
    }
}
