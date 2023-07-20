using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel;
using SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel.Guide;
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
    /// Interaction logic for DateForReportWindow.xaml
    /// </summary>
    public partial class DateForReportWindow : Window
    {
        public DateForReportViewModel DateForReportViewModel { get; set; }
        public DateForReportWindow()
        {
            InitializeComponent();
            this.DateForReportViewModel = new DateForReportViewModel();
            this.DataContext = this.DateForReportViewModel;
        }
    }
}
