using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using SIMS_HCI_Project_Group_5_Team_B.View;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View.Guide;
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

namespace SIMS_HCI_Project_Group_5_Team_B.View
{
    public partial class GuideWindow : Window
    {
        public GuideViewModel GuideViewModel { get; set; }
        public ViewModel ViewModel { get; set; }
        public GuideWindow(Guide guide)
        {
            InitializeComponent();
            this.GuideViewModel = new GuideViewModel(guide, this.frame.NavigationService, frame);
            this.ViewModel = new ViewModel();
            this.DataContext = this.GuideViewModel;
        }
    }
}