using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Repository
{
    public class TourAttendanceCSVRepository : CSVRepository<TourAttendance>, ITourAttendanceRepository
    {
        public TourAttendanceCSVRepository() : base() { }

        public void Delete(TourAttendance tourAttendance)
        {
            TourAttendance? founded = _data.Find(d => d.Id == tourAttendance.Id);
            if (founded != null)
            {
                _data.Remove(founded);
                WriteCSV(_data);
            }
        }

        public List<TourAttendance> GetAll()
        {
            return _data;
        }

        public void Save(TourAttendance newTourAttendance)
        {
            newTourAttendance.Id = NextId();
            _data.Add(newTourAttendance);
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

        public void Update(TourAttendance tourAttendance)
        {
            TourAttendance? current = _data.Find(d => d.Id == tourAttendance.Id);
            if (current == null)
            {
                Save(tourAttendance);
                return;
            }
            int index = _data.IndexOf(current);
            _data.Remove(current);
            _data.Insert(index, tourAttendance);
            WriteCSV(_data);
        }

        protected override TourAttendance FromCSV(string[] values)
        {
            TourAttendance newTourAttendance = new TourAttendance();

            newTourAttendance.Id = int.Parse(values[0]);
            newTourAttendance.GuideGuestId = int.Parse(values[1]);
            newTourAttendance.AppointmentId = int.Parse(values[2]);
            newTourAttendance.PeopleAttending = int.Parse(values[3]);
            newTourAttendance.KeyPointGuestArrivedId = int.Parse(values[4]);

            return newTourAttendance;
        }

        protected override string[] ToCSV(TourAttendance obj)
        {
            string[] csvValues =
            {
                obj.Id.ToString(),
                obj.GuideGuestId.ToString(),
                obj.AppointmentId.ToString(),
                obj.PeopleAttending.ToString(),
                obj.KeyPointGuestArrivedId.ToString()
            };

            return csvValues;
        }
    }
}
