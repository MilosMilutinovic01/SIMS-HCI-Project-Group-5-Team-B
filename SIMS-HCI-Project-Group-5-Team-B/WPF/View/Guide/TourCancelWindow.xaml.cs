using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class TourCancelWindow : Window
    {
        private AppointmentService appointmentService;
        private VoucherService voucherService;
        public ObservableCollection<Appointment> AvailableAppointments { get; }
        public Appointment SelectedAppointment { get; set; }
        public TourCancelWindow(Guide LogedInGuide)
        {
            InitializeComponent();
            this.DataContext = this;

            appointmentService = new AppointmentService();
            voucherService = new VoucherService();

            AvailableAppointments = new ObservableCollection<Appointment>();

            RefreshAppointments();
        }

        private void RefreshAppointments()
        {
            AvailableAppointments.Clear();
            foreach (Appointment appointment in appointmentService.GetUpcoming())
            {
                AvailableAppointments.Add(appointment);
            }
        }

        private void CancelTourButton_Click(object sender, RoutedEventArgs e)
        {
            bool result = MessageBox.Show("Are you sure you want to cancel selected tour?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
            if (result && SelectedAppointment.Cancelled == false)
            {
                SelectedAppointment.Cancelled = true;
                appointmentService.Update(SelectedAppointment);
                voucherService.SendVouchers(SelectedAppointment.Id);
                RefreshAppointments();
            }
            else
            {
                MessageBox.Show("Already cancelled!");
            }
        }
    }
}