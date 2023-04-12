using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Repository
{
    public class TourGradeCSVRepository : ITourGradeRepository
    {
        private Repository<TourGrade> tourGradeRepository;

        public TourGradeCSVRepository()
        {
            tourGradeRepository = new Repository<TourGrade>();
        }

        public void Delete(TourGrade tourGrade)
        {
            tourGradeRepository.Delete(tourGrade);
        }

        public List<TourGrade> GetAll()
        {
            return tourGradeRepository.GetAll();
        }

        public void Save(TourGrade newTourGrade)
        {
            tourGradeRepository.Save(newTourGrade);
        }

        public void Update(TourGrade tourGrade)
        {
            tourGradeRepository.Update(tourGrade);
        }
    }
}
