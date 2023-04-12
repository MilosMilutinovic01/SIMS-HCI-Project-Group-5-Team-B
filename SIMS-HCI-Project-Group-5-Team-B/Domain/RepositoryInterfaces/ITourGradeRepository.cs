using Microsoft.Win32;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces
{
    public interface ITourGradeRepository
    {
        public void Save(TourGrade newTourGrade);

        public void Delete(TourGrade tourGrade);

        public void Update(TourGrade tourGrade);

        public List<TourGrade> GetAll();
    }
}
