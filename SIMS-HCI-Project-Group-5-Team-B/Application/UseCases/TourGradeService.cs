using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Application.UseCases
{
    public class TourGradeService
    {
        private ITourGradeRepository tourGradeRepository;
        TourAttendanceService tourAttendanceService;
        AppointmentService appointmentService;

        public TourGradeService()
        {
            this.tourGradeRepository = Injector.Injector.CreateInstance<ITourGradeRepository>();
            this.tourAttendanceService = new TourAttendanceService();
            this.appointmentService = new AppointmentService();
        }

        public void Save(TourGrade tourGrade)
        {
            tourGradeRepository.Save(tourGrade);
        }
        public void Update(TourGrade tourGrade)
        {
            tourGradeRepository.Update(tourGrade);
        }
        public TourGrade? GetGradeFor(int guideGuestId, int tourAttendanceId)
        {
            return tourGradeRepository.GetAll().Find(tg => ((tg.GuideGuestId == guideGuestId) && (tg.TourAttendanceId == tourAttendanceId)));
        }

        public bool IsRated(int guideGuestId, int tourAttendanceId)
        {
            return GetGradeFor(guideGuestId, tourAttendanceId) != null;
        }

        public double GetAverageGrade(int guideId, string language)
        {
            double averageGrade = 0;
            int counter = 0;
            foreach(TourGrade tg in tourGradeRepository.GetAll())
            {
                TourAttendance ta = tourAttendanceService.GetById(tg.TourAttendanceId);
                Appointment a = appointmentService.getById(ta.AppointmentId);
                if(a.GuideId == guideId && a.Tour.Language.Equals(language))
                {
                    averageGrade += (double)(tg.GuideGeneralKnowledge + tg.GuideLanguageKnowledge + tg.TourFun) / 3;
                    counter += 1;
                }
            }
            if(counter!=0)
                return averageGrade / counter;
            return averageGrade;
        }
        public List<TourGrade> GetAll()
        {
            return tourGradeRepository.GetAll();
        }

        public TourGrade getById(int id)
        {
            return GetAll().Find(tg => tg.Id == id);
        }
    }
}
