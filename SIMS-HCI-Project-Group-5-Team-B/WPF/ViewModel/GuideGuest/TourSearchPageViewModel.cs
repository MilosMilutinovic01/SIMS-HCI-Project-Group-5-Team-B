using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Xceed.Wpf.Toolkit.Primitives;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel.GuideGuest
{
    public class TourSearchPageViewModel
    {
        public ObservableCollection<Tour> Tours { get; set; }


        public List<string> States { get; set; }
        private string selectedState;
        public string SelectedState
        {
            get => selectedState;
            set
            {
                selectedState = value;
                Cities.Clear();
                foreach(var city in locationController.GetCityByState(selectedState))
                {
                    Cities.Add(city);
                }
            }
        }
        public ObservableCollection<String> Cities { get; set; }
        public string SelectedCity { get; set; }
        public int NumberOfGuests { get; set; }
        public string Language { get; set; }
        public int Duration { get; set; }


        public RelayCommand SearchCommand { get; }
        public RelayCommand ResetCommand { get; }


        private LocationController locationController;
        public TourSearchPageViewModel()
        {
            locationController = new LocationController();

            Tours = new ObservableCollection<Tour>((new TourService(new TourCSVRepository(new KeyPointCSVRepository(), new LocationCSVRepository()))).GetAll());

            States = locationController.GetStates();
            Cities = new ObservableCollection<string>();

            SearchCommand = new RelayCommand(Search_Execute, CanSearch);
            ResetCommand = new RelayCommand(Reset_Execute);
        }






        public bool CanSearch()
        {
            return NumberOfGuests > 0 && Duration > 0;
        }
        public void Search_Execute()
        {
            //Search Tours
        }

        public void Reset_Execute()
        {
            NumberOfGuests = 0;
            Duration = 0;
            Language = String.Empty;
            SelectedCity = String.Empty;
            SelectedState = String.Empty;
        }
    }
}
