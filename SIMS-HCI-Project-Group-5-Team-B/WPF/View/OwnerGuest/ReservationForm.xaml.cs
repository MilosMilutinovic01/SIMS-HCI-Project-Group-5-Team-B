using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace SIMS_HCI_Project_Group_5_Team_B.View
{
    /// <summary>
    /// Interaction logic for ReservationFormWindow.xaml
    /// </summary>
    public partial class ReservationForm : Window
    {
        public ReservationForm(ReservationService reservationService, Accommodation SelectedAccomodation,int ownerGuestId)
        {
            InitializeComponent();
            this.DataContext = new ReservationFormViewModel(reservationService,SelectedAccomodation,ownerGuestId);

        }

    }
}
