using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Repository
{
    public class GuideGuestCSVRepository : CSVRepository<GuideGuest>, IGuideGuestRepository
    {
        public void Delete(GuideGuest guideGuest)
        {
            GuideGuest? founded = _data.Find(d => d.Id == guideGuest.Id);
            if (founded != null)
            {
                _data.Remove(founded);
                WriteCSV(_data);
            }
        }

        public void Save(GuideGuest newGuideGuest)
        {
            newGuideGuest.Id = NextId();
            _data.Add(newGuideGuest);
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

        public void Update(GuideGuest guideGuest)
        {
            GuideGuest? current = _data.Find(d => d.Id == guideGuest.Id);
            if (current == null)
            {
                Save(guideGuest);
                return;
            }
            int index = _data.IndexOf(current);
            _data.Remove(current);
            _data.Insert(index, guideGuest);
            WriteCSV(_data);
        }

        List<GuideGuest> GetAll()
        {
            return _data;
        }

        protected override GuideGuest FromCSV(string[] values)
        {
            GuideGuest newGuideGuest = new GuideGuest();

            newGuideGuest.Id = int.Parse(values[0]);
            newGuideGuest.Name = values[1];
            newGuideGuest.Surname = values[2];
            newGuideGuest.Age = int.Parse(values[3]);

            return newGuideGuest;
        }

        protected override string[] ToCSV(GuideGuest obj)
        {
            string[] csvValues =
{
                obj.Id.ToString(),
                obj.Name,
                obj.Surname,
                obj.Age.ToString(),
            };

            return csvValues;
        }

        List<GuideGuest> IGuideGuestRepository.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
