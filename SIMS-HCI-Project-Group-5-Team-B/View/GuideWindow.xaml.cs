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

namespace SIMS_HCI_Project_Group_5_Team_B.View
{
    /// <summary>
    /// Interaction logic for GuideWindow.xaml
    /// </summary>
    public partial class GuideWindow : Window
    {
        public GuideWindow()
        {
            InitializeComponent();
        }

        private void AddTourClick(object sender, RoutedEventArgs e)
        {
            TourForm tourForm = new TourForm();
            tourForm.Show();
        }

        private void TrackinTourLiveClick(object sender, RoutedEventArgs e)
        {

        }
    }
}