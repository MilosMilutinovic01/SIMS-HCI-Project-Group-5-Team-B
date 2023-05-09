﻿using System;
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
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel;
using System.Collections.ObjectModel;
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.View
{
    /// <summary>
    /// Interaction logic for CallOffRenovationWindow.xaml
    /// </summary>
    public partial class CallOffRenovationWindow : Window
    {

        private readonly RenovationViewModel renovationViewModel;
        public ObservableCollection<RenovationGridView> FutureRenovations { get; set; }
        public RenovationGridView SelectedRenovationGridView { get; set; }
        public CallOffRenovationWindow(RenovationGridView SelectedRenovationGridView,ObservableCollection<RenovationGridView> FutureRenovations, RenovationService renovationService, ReservationService reservationService, int ownerId)
        {
            InitializeComponent();
            this.SelectedRenovationGridView = SelectedRenovationGridView;
            renovationViewModel = new RenovationViewModel(renovationService, reservationService, ownerId,SelectedRenovationGridView);
            this.FutureRenovations = FutureRenovations;
            
        }

        private void Confirm_Button_Click(object sender, RoutedEventArgs e)
        {
            renovationViewModel.CallOff(SelectedRenovationGridView);
            FutureRenovations.Remove(SelectedRenovationGridView);
            Close();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
