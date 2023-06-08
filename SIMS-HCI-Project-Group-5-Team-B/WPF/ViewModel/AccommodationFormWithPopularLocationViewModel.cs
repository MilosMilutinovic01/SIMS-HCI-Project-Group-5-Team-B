using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class AccommodationFormWithPopularLocationViewModel
    {

        public Location SelectedLocation { get; set; }
        public Owner Owner { get; set; }
        public Accommodation Accommodation { get; set; }
        public ObservableCollection<Accommodation> AccommodationsOfLogedInOwner { get; set; }
        private AccommodationService accommodationService;
        public RelayCommand CloseCommand { get; }
        public RelayCommand CreateAccommodation { get; }
        public AccommodationFormWithPopularLocationViewModel(Location location, ObservableCollection<Accommodation> accommodationsOfLogedInOwner,Owner owner,AccommodationService accommodationService)
        {
            this.SelectedLocation = location;
            this.AccommodationsOfLogedInOwner = accommodationsOfLogedInOwner;
            this.Owner = owner;
            this.accommodationService = accommodationService;
            Accommodation = new Accommodation();
            CloseCommand = new RelayCommand(Cancel_Exexute, CanExecute);
            CreateAccommodation = new RelayCommand(CreateAccommodationExecute, CanExecute);
        }

        public void Cancel_Exexute()
        {
            App.Current.Windows.OfType<AccommodationFormWithPopularLocation>().FirstOrDefault().Close();
        }

        public bool CanExecute()
        {
            return true;
        }

        private void CreateAccommodationExecute()
        {
            if (Accommodation.IsValid)
            {
                Accommodation.OwnerId = Owner.Id;
                Accommodation.LocationId = SelectedLocation.Id;
                accommodationService.Save(Accommodation);
                AccommodationsOfLogedInOwner.Add(Accommodation);
                Cancel_Exexute();
            }
            else
            {
                if (Properties.Settings.Default.currentLanguage == "en-US")
                {
                    MessageBox.Show("Accommodation can't be created, because fileds are not valid");
                }
                else
                {
                    MessageBox.Show("Smestaj ne moze biti kreiran, jer polja nisu validna");
                }

            }
        }

    }
}
