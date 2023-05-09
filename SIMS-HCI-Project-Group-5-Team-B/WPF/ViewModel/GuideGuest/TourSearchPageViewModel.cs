using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel.GuideGuest
{
    public class TourSearchPageViewModel
    {
        public ObservableCollection<Tour> Tours { get; set; }

        public TourSearchPageViewModel()
        {
            Tours = new ObservableCollection<Tour>((new TourService(new TourCSVRepository(new KeyPointCSVRepository(), new LocationCSVRepository()))).GetAll());
        }
    }
}
