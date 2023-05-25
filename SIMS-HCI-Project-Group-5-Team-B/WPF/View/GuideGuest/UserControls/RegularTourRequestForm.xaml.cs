using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel.GuideGuest;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.View.GuideGuest.UserControls
{
    /// <summary>
    /// Interaction logic for RegularTourRequestForm.xaml
    /// </summary>
    public partial class RegularTourRequestForm : UserControl
    {
        public TourRequest TourRequest
        {
            get { return (TourRequest)GetValue(TourRequestProperty); }
            set { SetValue(TourRequestProperty, value); }
        }

        public static readonly DependencyProperty TourRequestProperty =
            DependencyProperty.Register("TourRequest", typeof(TourRequest), typeof(RegularTourRequestForm),
                new PropertyMetadata(null));

        public ICommand SaveCommand
        {
            get { return (ICommand)GetValue(SaveCommandProperty); }
            set { SetValue(SaveCommandProperty, value); }
        }

        public static readonly DependencyProperty SaveCommandProperty =
            DependencyProperty.Register("SaveCommand", typeof(ICommand), typeof(RegularTourRequestForm),
                new PropertyMetadata(null));

        public ICommand CancelCommand
        {
            get { return (ICommand)GetValue(CancelCommandProperty); }
            set { SetValue(CancelCommandProperty, value); }
        }

        public static readonly DependencyProperty CancelCommandProperty =
            DependencyProperty.Register("CancelCommand", typeof(ICommand), typeof(RegularTourRequestForm),
                new PropertyMetadata(null));



        public RegularTourRequestForm()
        {
            InitializeComponent();
        }
    }
}
