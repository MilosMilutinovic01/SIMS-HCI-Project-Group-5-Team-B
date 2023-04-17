using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;

namespace SIMS_HCI_Project_Group_5_Team_B.View
{
    /// <summary>
    /// Interaction logic for AccomodationDetailsWindow.xaml
    /// </summary>
    public partial class AccomodationDetailsWindow : Window
    {
        public Accommodation SelectedAccommodation { get; set; }
        private ReservationService reservationService;
        private int ownerGuestId;
        public AccomodationDetailsWindow(Accommodation SelectedAccomodation, ReservationService reservationService, int ownerGuestId)
        {
            InitializeComponent();
            DataContext = this;
            this.SelectedAccommodation = SelectedAccomodation;
            this.reservationService = reservationService;
            this.ownerGuestId = ownerGuestId;
            ShowImages();

        }

        private void ShowImages()
        {
            imageListBox.Items.Clear();
            
            foreach(String imageSource in SelectedAccommodation.pictureURLs)
            {
                imageListBox.Items.Add(imageSource);
            }
        }

        private void Reserve_Button_Click(object sender, RoutedEventArgs e)
        {
            ReservationForm reservationForm = new ReservationForm(reservationService, SelectedAccommodation, ownerGuestId);
            reservationForm.Show();

        }
    }
}
