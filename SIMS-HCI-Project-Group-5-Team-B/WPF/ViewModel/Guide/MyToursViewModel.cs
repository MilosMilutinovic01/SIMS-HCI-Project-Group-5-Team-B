using ceTe.DynamicPDF;
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View.Guide;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ceTe.DynamicPDF;
using ceTe.DynamicPDF.PageElements;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class MyToursViewModel : ViewModel
    {
        #region fields
        private string selectedYear;
        public string SelectedYear 
        {
            get { return selectedYear; }
            set
            {
                if(selectedYear != value) 
                {
                    selectedYear = value;
                    RefreshData();
                }
            }
        }
        public RelayCommand ShowStatisticsCommand { get; set; }
        public RelayCommand ReportOnScheduledToursCommand { get; set; }
        public ObservableCollection<string> Years { get; set; }
        public ObservableCollection<Appointment> MostVisitedAppointment { get; set; }
        public Appointment SelectedAppointment { get; set; }
        public ObservableCollection<Appointment> FinishedAppointments { get; set; }
        public int userId;

        private AppointmentService appointmentService;
        private TourAttendanceService tourAttendanceService;


        #endregion

        #region actions
        private bool CanExecute_NavigateCommand()
        {
            return true;
        }

        private void Execute_ShowStatisticsCommand()
        {
            if (SelectedAppointment == null)
            {
                MessageBox.Show("You must select appointment!");
                return;
            }
            TourStatistics tourStatistics = new TourStatistics(SelectedAppointment.Id, tourAttendanceService);
            tourStatistics.Show();
        }

        private void Execute_ReportOnScheduledToursCommand()
        {
            Document document = new Document();

            ceTe.DynamicPDF.Page page = new ceTe.DynamicPDF.Page(PageSize.Letter, PageOrientation.Portrait, 54.0f);
            document.Pages.Add(page);

            string labelText = "Hello World...\nFrom DynamicPDF Generator for .NET\nDynamicPDF.com";
            string labelText1 = "TEST...\nTEST\nTEST";
            ceTe.DynamicPDF.PageElements.Label label = new ceTe.DynamicPDF.PageElements.Label(labelText, 0, 0, 504, 100, Font.Helvetica, 18, TextAlign.Center);
            ceTe.DynamicPDF.PageElements.Label label1 = new ceTe.DynamicPDF.PageElements.Label(labelText1, 0, 100, 504, 100, Font.Helvetica, 18, TextAlign.Center);
            page.Elements.Add(label);
            page.Elements.Add(label1);

            string projectFolderPath = System.IO.Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
            string folderPath = System.IO.Path.Combine(projectFolderPath, "Resources");
            string filePath = System.IO.Path.Combine(folderPath, "ReportOnScheduledTours.pdf");

            document.Draw(filePath);
        }
        #endregion
        public MyToursViewModel(int userId)
        {
            this.userId = userId;
            KeyPointCSVRepository keyPointCSVRepository = new KeyPointCSVRepository();
            LocationCSVRepository locationCSVRepository = new LocationCSVRepository();
            TourCSVRepository tourCSVRepository = new TourCSVRepository();
            TourAttendanceCSVRepository tourAttendanceCSVRepository = new TourAttendanceCSVRepository();
            TourGradeCSVRepository tourGradeCSVRepository = new TourGradeCSVRepository();
            AppointmentCSVRepository appointmentCSVRepository = new AppointmentCSVRepository();

            this.tourAttendanceService = new TourAttendanceService();
            this.appointmentService = new AppointmentService();

            MostVisitedAppointment = new ObservableCollection<Appointment>();
            FinishedAppointments = new ObservableCollection<Appointment>();

            this.ShowStatisticsCommand = new RelayCommand(Execute_ShowStatisticsCommand, CanExecute_NavigateCommand);
            this.ReportOnScheduledToursCommand = new RelayCommand(Execute_ReportOnScheduledToursCommand, CanExecute_NavigateCommand);
            Years = new ObservableCollection<string>();
            foreach(string year in appointmentService.GetAllYears())
                Years.Add(year);

            PageName = "My tours";

            RefreshData();
        }

        private void RefreshData()
        {
            MostVisitedAppointment.Clear();
            FinishedAppointments.Clear();

            MostVisitedAppointment.Add(appointmentService.GetMostVisitedTour(Convert.ToInt32(SelectedYear), userId));

            if (appointmentService.GetFinishedToursByYear(Convert.ToInt32(SelectedYear), userId) == null)
                FinishedAppointments.Add(new Appointment());
            else
                foreach (Appointment a in appointmentService.GetFinishedToursByYear(Convert.ToInt32(SelectedYear), userId))
                    FinishedAppointments.Add(a);
        }
    }
}
