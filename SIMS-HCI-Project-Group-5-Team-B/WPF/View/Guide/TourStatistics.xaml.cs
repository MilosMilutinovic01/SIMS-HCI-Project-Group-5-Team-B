using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
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
        private AppointmentService appointmentService;
        private TourAttendanceService tourAttendanceService;
        private VoucherService voucherService;
        public TourStatistics(int appointmentId, AppointmentService appointmentService, TourAttendanceService tourAttendanceService)
        {
            InitializeComponent();
            this.DataContext = this;

            TourAttendanceCSVRepository tourAttendanceCSVRepository = new TourAttendanceCSVRepository();
            tourController = new TourController();
            voucherService = new VoucherService();

            this.appointmentService = appointmentService;
            this.tourAttendanceService = tourAttendanceService;

            TourName.Content = tourController.getById(appointmentId).Name;
            int totalGuests = tourAttendanceService.GetTotalGuest(appointmentId);
            TotalGuests.Content = "Total guests: " + totalGuests;

            Under18.Content = "Guests under 18 years: 0";
            Between.Content = "Guests between 18 and 50 years: " + totalGuests;
            Above50.Content = "Guests above 50 years: 0";   //hardcoded because of lack of this information in data

            double withVouchers = (double)tourAttendanceService.GetNumberOfGuestsWithVoucher(voucherService.GetAllGuests()) / totalGuests * 100;
            double withoutVouchers = (double)(totalGuests - tourAttendanceService.GetNumberOfGuestsWithVoucher(voucherService.GetAllGuests())) / totalGuests * 100;
            WithVoucher.Content = "Guests with voucher on this tour: " + withVouchers.ToString("F2") + "%";
            WithoutVoucher.Content = "Guests without voucher on this tour: " + withoutVouchers.ToString("F2") + "%";
        }
    }
}
