using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
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
    /// Interaction logic for TourStatistics.xaml
    /// </summary>
    public partial class TourStatistics : Window
    {
        private TourController tourController;
        private TourAttendanceService tourAttendanceService;
        public TourStatistics(int id)
        {
            InitializeComponent();
            this.DataContext = this;

            tourController = new TourController();
            tourAttendanceService = new TourAttendanceService();

            TourName.Content = tourController.getById(id).Name;
            if (tourAttendanceService.GetByTourAppointmentId(id).PeopleAttending != null)
                TotalGuests.Content = tourAttendanceService.GetByTourAppointmentId(id).;
            else
                TotalGuests.Content = "0";    
            //WithVoucher.Content = "1";
            //WithoutVoucher.Content = "2";
        }
    }
}
