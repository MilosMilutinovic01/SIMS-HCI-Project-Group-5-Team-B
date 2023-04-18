using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Repository
{
    public class AppointmentCSVRepository : CSVRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentCSVRepository(ITourRepository tourRepository) : base()
        {
            LoadTour(tourRepository);
        }
        private void LoadTour(ITourRepository tourRepository)
        {
            foreach(var appointment in _data)
            {
                appointment.Tour = tourRepository.GetAll().Find(t => t.Id == appointment.TourId);
            }
        }

        public void Delete(Appointment appointment)
        {
            Appointment? founded = _data.Find(d => d.Id == appointment.Id);
            if (founded != null)
            {
                _data.Remove(founded);
                WriteCSV(_data);
            }
        }

        public List<Appointment> GetAll()
        {
            return _data;
        }

        public void Save(Appointment newAppointment)
        {
            newAppointment.Id = NextId();
            _data.Add(newAppointment);
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

        public void Update(Appointment appointment)
        {
            Appointment? current = _data.Find(d => d.Id == appointment.Id);
            if (current == null)
            {
                Save(appointment);
                return;
            }
            int index = _data.IndexOf(current);
            _data.Remove(current);
            _data.Insert(index, appointment);
            WriteCSV(_data);
        }

        protected override Appointment FromCSV(string[] values)
        {
            Appointment newAppointment = new Appointment();

            newAppointment.Id = int.Parse(values[0]);
            newAppointment.TourId = int.Parse(values[1]);
            newAppointment.GuideId = int.Parse(values[2]);
            newAppointment.Start = Convert.ToDateTime(values[3], CultureInfo.GetCultureInfo("en-US"));
            newAppointment.FreeSpace = int.Parse(values[4]);
            newAppointment.Started = Convert.ToBoolean(values[5]);
            newAppointment.Ended = Convert.ToBoolean(values[6]);
            newAppointment.Cancelled = Convert.ToBoolean(values[7]);
            newAppointment.CheckedKeyPointId = int.Parse(values[8]);

            return newAppointment;
        }

        protected override string[] ToCSV(Appointment obj)
        {
            string[] csvValues =
{
                obj.Id.ToString(),
                obj.TourId.ToString(),
                obj.GuideId.ToString(),
                obj.Start.ToString(CultureInfo.GetCultureInfo("en-US")),
                obj.FreeSpace.ToString(),
                obj.Started.ToString(),
                obj.Ended.ToString(),
                obj.Cancelled.ToString(),
                obj.CheckedKeyPointId.ToString(),
            };
            return csvValues;
        }
    }
}
