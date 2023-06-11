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
        private VoucherService voucherService;
        private AppointmentService appointmentService;
        private TourAttendanceService tourAttendanceService;
        public TourStatistics(int appointmentId, TourAttendanceService tourAttendanceService)
        {
            InitializeComponent();
            this.DataContext = this;

            TourAttendanceCSVRepository tourAttendanceCSVRepository = new TourAttendanceCSVRepository();
            tourController = new TourController();
            voucherService = new VoucherService();
            appointmentService = new AppointmentService();

            this.tourAttendanceService = tourAttendanceService;;

            TourName.Content = appointmentService.getById(appointmentId).Tour.Name;
            int totalGuests = tourAttendanceService.GetTotalGuest(appointmentId);
            TotalGuests.Content = "Total guests: " + totalGuests;

            Under18.Content = "Guests under 18 years: 0";
            Between.Content = "Guests between 18 and 50 years: " + totalGuests;
            Above50.Content = "Guests above 50 years: 0";   //hardcoded because of lack of this information in data

            double withVouchers = 0;
            double withoutVouchers = 0;
            if (totalGuests != 0)
            {
                withVouchers = (double)tourAttendanceService.GetNumberOfGuestsWithVoucher(voucherService.GetAllGuests(), totalGuests) / totalGuests * 100;
                withoutVouchers = (double)(totalGuests - tourAttendanceService.GetNumberOfGuestsWithVoucher(voucherService.GetAllGuests(), totalGuests)) / totalGuests * 100;
            }
            else
            {
                withVouchers = 0;
                withoutVouchers = 0;
            }
            if(totalGuests != 0)
            WithVoucher.Content = "Guests with voucher on this tour: " + withVouchers.ToString("F2") + "%";
            WithoutVoucher.Content = "Guests without voucher on this tour: " + withoutVouchers.ToString("F2") + "%";
        }
    }
}
