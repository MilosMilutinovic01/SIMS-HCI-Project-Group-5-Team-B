using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Model;
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
        private AppointmentController appointmentController;
        public ObservableCollection<Appointment> AvailableAppointments { get; }
        public Appointment SelectedAppointment { get; set; }
        public TourCancelWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            appointmentController = new AppointmentController();

            AvailableAppointments = new ObservableCollection<Appointment>();

            RefreshAppointments();
        }

        private void RefreshAppointments()
        {
            AvailableAppointments.Clear();
            foreach (Appointment appointment in appointmentController.GetUpcoming())
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
                appointmentController.Update(SelectedAppointment);
            }
            else
            {
                MessageBox.Show("Already cancelled!");
            }
        }
    }
}