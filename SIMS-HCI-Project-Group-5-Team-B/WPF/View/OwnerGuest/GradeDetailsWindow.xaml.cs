﻿using SIMS_HCI_Project_Group_5_Team_B.DTO;
using SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel;
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

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.View.OwnerGuest
{
    /// <summary>
    /// Interaction logic for GradeDetailsWindow.xaml
    /// </summary>
    public partial class GradeDetailsWindow : Window
    {
        public GradeDetailsWindow(OwnerGuestGradesDTO SelectedGrade)
        {
            InitializeComponent();
            this.DataContext = new OwnerGuestGradeDetailsViewModel(SelectedGrade);
        }

    }
}
