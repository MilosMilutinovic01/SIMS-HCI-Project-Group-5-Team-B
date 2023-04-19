using Microsoft.Win32;
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class TourRateViewModel
    {
        private TourGradeService tourGradeService;
        public TourRateViewModel(TourGradeService tourGradeService)
        {
            this.tourGradeService = tourGradeService;
        }

        public void Save(TourGrade tourGrade)
        {
            tourGradeService.Save(tourGrade);
        }
    }
}
