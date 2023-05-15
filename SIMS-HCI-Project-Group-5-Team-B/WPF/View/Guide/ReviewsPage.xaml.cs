using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.View.Guide
{
    /// <summary>
    /// Interaction logic for ReviewsPage.xaml
    /// </summary>
    public partial class ReviewsPage : Page
    {
        public ReviewsViewModel reviewsViewModel { get; set; }
        public ReviewsPage()//probaj da dodas parametre i da pokupis kroz binding(potrazi na gimu mozda ima u primeru)
        {
            InitializeComponent();
            this.reviewsViewModel = new ReviewsViewModel(1);
            this.DataContext = this.reviewsViewModel;
        }
    }
}
