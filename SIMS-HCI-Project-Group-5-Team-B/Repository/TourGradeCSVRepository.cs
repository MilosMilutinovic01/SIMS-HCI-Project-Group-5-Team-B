using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using SIMS_HCI_Project_Group_5_Team_B.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Repository
{
    public class TourGradeCSVRepository : CSVRepository<TourGrade>, ITourGradeRepository
    {
        public TourGradeCSVRepository() : base() { }

        public void Delete(TourGrade tourGrade)
        {
            TourGrade? founded = _data.Find(d => d.Id == tourGrade.Id);
            if (founded != null)
            {
                _data.Remove(founded);
                WriteCSV(_data);
            }
        }

        public void Save(TourGrade newTourGrade)
        {
            newTourGrade.Id = NextId();
            _data.Add(newTourGrade);
            WriteCSV(_data);
        }
        private int NextId()
        {
            if(_data.Count() < 1)
            {
                return 1;
            }
            else
            {
                return _data.Max(d => d.Id) + 1;
            }
        }

        public void Update(TourGrade tourGrade)
        {
            TourGrade? current = _data.Find(d => d.Id == tourGrade.Id);
            if (current == null)
            {
                Save(tourGrade);
                return;
            }
            int index = _data.IndexOf(current);
            _data.Remove(current);
            _data.Insert(index, tourGrade);
            WriteCSV(_data);
        }

        public List<TourGrade> GetAll()
        {
            return _data;
        }

        protected override string[] ToCSV(TourGrade obj)
        {
            string[] csvValues =
            {
                obj.Id.ToString(),
                obj.GuideGeneralKnowledge.ToString(),
                obj.GuideLanguageKnowledge.ToString(),
                obj.TourFun.ToString(),
                obj.ImageUrls,
                obj.TourAttendanceId.ToString(),
                obj.GuideGuestId.ToString(),
            };
            return csvValues;
        }

        protected override TourGrade FromCSV(string[] values)
        {
            TourGrade newTourGrade = new TourGrade();

            newTourGrade.Id = int.Parse(values[0]);
            newTourGrade.GuideGeneralKnowledge = int.Parse(values[1]);
            newTourGrade.GuideLanguageKnowledge = int.Parse(values[2]);
            newTourGrade.TourFun = int.Parse(values[3]);
            newTourGrade.ImageUrls = values[4];
            newTourGrade.TourAttendanceId = int.Parse(values[5]);
            newTourGrade.GuideGuestId = int.Parse(values[6]);

            return newTourGrade;
        }
    }
}
