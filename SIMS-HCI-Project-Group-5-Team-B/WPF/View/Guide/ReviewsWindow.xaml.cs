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

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.View.Guide
{
    /// <summary>
    /// Interaction logic for ReviewsWindow.xaml
    /// </summary>
    public partial class ReviewsWindow : Window
    {
        private ReviewsViewModel reviewsViewModel;
        public ReviewsWindow()
        {
            InitializeComponent();

            reviewsViewModel = new ReviewsViewModel();
            this.DataContext = reviewsViewModel;
        }
    }
}
