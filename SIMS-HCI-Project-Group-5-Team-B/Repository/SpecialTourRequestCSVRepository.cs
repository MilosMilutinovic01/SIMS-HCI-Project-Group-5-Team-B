using SIMS_HCI_Project_Group_5_Team_B.Application.Injector;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Repository
{
    public class SpecialTourRequestCSVRepository : CSVRepository<SpecialTourRequest>, ISpecialTourRequestsRepository
    {
        public SpecialTourRequestCSVRepository() : base() { }

        public void LoadData()
        {
            ITourRequestRepository tourRequestRepository = Injector.CreateInstance<ITourRequestRepository>();
            foreach (var request in tourRequestRepository.GetAll())
            {
                if (request.SpecialTourId == -1) continue;
                foreach(var specialRequest in _data)
                {
                    if(specialRequest.Id == request.SpecialTourId)
                    {
                        specialRequest.TourRequests.Add(request);
                    }
                }
            }
        }

        public void Delete(SpecialTourRequest specialTourRequest)
        {
            SpecialTourRequest? founded = _data.Find(d => d.Id == specialTourRequest.Id);
            if (founded != null)
            {
                _data.Remove(founded);
                WriteCSV(_data);
            }
        }

        public List<SpecialTourRequest> GetAll()
        {
            return _data;
        }

        public void Save(SpecialTourRequest newSpecialTourRequest)
        {
            newSpecialTourRequest.Id = NextId();
            _data.Add(newSpecialTourRequest);
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

        public void Update(SpecialTourRequest specialTourRequest)
        {
            SpecialTourRequest? current = _data.Find(d => d.Id == specialTourRequest.Id);
            if (current == null)
            {
                Save(specialTourRequest);
                return;
            }
            int index = _data.IndexOf(current);
            _data.Remove(current);
            _data.Insert(index, specialTourRequest);
            WriteCSV(_data);
        }

        protected override SpecialTourRequest FromCSV(string[] values)
        {
            SpecialTourRequest newSpecialTourRequest = new SpecialTourRequest();

            newSpecialTourRequest.Id = int.Parse(values[0]);
            newSpecialTourRequest.Name = values[1];

            return newSpecialTourRequest;
        }

        protected override string[] ToCSV(SpecialTourRequest obj)
        {
            string[] csvValues =
            {
                obj.Id.ToString(),
                obj.Name.ToString(),
            };
            return csvValues;
        }
    }
}
