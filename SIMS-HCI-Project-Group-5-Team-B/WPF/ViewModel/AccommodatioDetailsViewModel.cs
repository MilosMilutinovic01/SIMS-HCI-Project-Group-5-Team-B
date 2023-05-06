using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using SIMS_HCI_Project_Group_5_Team_B.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class AccommodatioDetailsViewModel
    {
        public Accommodation SelectedAccommodation { get; set; }
        private ReservationService reservationService;
        private int ownerGuestId;

        public RelayCommand ReserveCommand { get; }
        public RelayCommand CancelCommand { get; }

        AccomodationDetailsWindow window;
        private ListBox imageListBox;
        public AccommodatioDetailsViewModel(Accommodation SelectedAccomodation, ReservationService reservationService, int ownerGuestId, AccomodationDetailsWindow window, ListBox imagesBox)
        {
            
            this.SelectedAccommodation = SelectedAccomodation;
            this.reservationService = reservationService;
            this.ownerGuestId = ownerGuestId;

            //commands
            ReserveCommand = new RelayCommand(Reserve_Execute, CanExecute);
            CancelCommand = new RelayCommand(Cancel_Execute, CanExecute);
            this.window = window;
            this.imageListBox = imagesBox;
            ShowImages();
        }


        private void ShowImages()
        {
            imageListBox.Items.Clear();

            foreach (String imageSource in SelectedAccommodation.pictureURLs)
            {
                imageListBox.Items.Add(imageSource);
            }
        }
        public void Reserve_Execute()
        {
            ReservationForm reservationForm = new ReservationForm(reservationService, SelectedAccommodation, ownerGuestId);
            reservationForm.Show();

        }

        private void Cancel_Execute()
        {
            window.Close();
        }

        public bool CanExecute()
        {
            return true;
        }
    }
}
