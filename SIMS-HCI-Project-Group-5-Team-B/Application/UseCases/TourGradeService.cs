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

        public TourGradeService()
        {
            this.tourGradeRepository = Injector.Injector.CreateInstance<ITourGradeRepository>();
        }

        public void Save(TourGrade tourGrade)
        {
            tourGradeRepository.Save(tourGrade);
        }
        public TourGrade? GetGradeFor(int guideGuestId, int tourAttendanceId)
        {
            return tourGradeRepository.GetAll().Find(tg => ((tg.GuideGuestId == guideGuestId) && (tg.TourAttendanceId == tourAttendanceId)));
        }

        public bool IsRated(int guideGuestId, int tourAttendanceId)
        {
            return GetGradeFor(guideGuestId, tourAttendanceId) != null;
        }

        public List<TourGrade> GetAll()
        {
            return tourGradeRepository.GetAll();
        }
    }
}
