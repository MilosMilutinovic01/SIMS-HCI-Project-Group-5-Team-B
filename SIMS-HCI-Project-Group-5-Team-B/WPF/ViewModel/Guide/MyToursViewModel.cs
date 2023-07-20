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
//using ceTe.DynamicPDF;
using iTextSharp.text;
using iTextSharp.text.pdf;
//using ceTe.DynamicPDF.PageElements;
using System.Data;
using System.Windows.Data;
using System.Windows.Documents;
using System.IO;
using System.Windows.Markup;
using Image = iTextSharp.text.Image;
using Font = iTextSharp.text.Font;
using Paragraph = iTextSharp.text.Paragraph;
using System.Diagnostics;
using System.Windows.Media.Effects;

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
        private bool isOpenedPopup;
        public bool IsOpenedPopup
        {
            get
            {
                return isOpenedPopup;
            }
            set
            {
                if (isOpenedPopup != value)
                {
                    isOpenedPopup = value;
                    OnPropertyChanged(nameof(IsOpenedPopup));
                }
            }
        }
        
        private bool isEnabledStatisticsButton;
        public bool IsEnabledStatisticsButton
        {
            get
            {
                return isEnabledStatisticsButton;
            }
            set
            {
                if (isEnabledStatisticsButton != value)
                {
                    isEnabledStatisticsButton = value;
                    OnPropertyChanged(nameof(IsEnabledStatisticsButton));
                }
            }
        }
        public RelayCommand ShowStatisticsCommand { get; set; }
        public RelayCommand ReportOnScheduledToursCommand { get; set; }
        public RelayCommand EnableStatisticsButtonCommand { get; set; }
        public RelayCommand OpenPopupCommand { get; set; }
        public ObservableCollection<string> Years { get; set; }
        public ObservableCollection<Appointment> MostVisitedAppointment { get; set; }
        public Appointment SelectedAppointment { get; set; }
        public ObservableCollection<Appointment> FinishedAppointments { get; set; }
        public int userId;

        private AppointmentService appointmentService;
        private TourAttendanceService tourAttendanceService;
        private GuideService guideService;


        #endregion

        #region actions
        private bool CanExecute_NavigateCommand()
        {
            return true;
        }
        private void Execute_OpenPopupCommand()
        {
            IsOpenedPopup = !IsOpenedPopup;
        }
        private void Execute_EnableStatisticsButtonCommand()
        {
            IsEnabledStatisticsButton = true;
        }
        private void Execute_ReportOnScheduledToursCommand()
        {
            Window window = System.Windows.Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            if (window != null)
            {
                window.Effect = new BlurEffect();
            }
            DateForReportWindow dateForReportWindow = new DateForReportWindow();
            dateForReportWindow.ShowDialog();
            window.Effect = null;
        }
        private void Execute_ShowStatisticsCommand()
        {
            if (SelectedAppointment == null)
            {
                MessageBox.Show("You must select appointment!");
                return;
            }
            Window window = System.Windows.Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            if (window != null)
            {
                window.Effect = new BlurEffect();
            }
            TourStatistics tourStatistics = new TourStatistics(SelectedAppointment.Id, tourAttendanceService);
            tourStatistics.ShowDialog();
            window.Effect = null;
        }
        #endregion
        public MyToursViewModel(int userId)
        {
            this.userId = userId;

            this.tourAttendanceService = new TourAttendanceService();
            this.appointmentService = new AppointmentService();
            this.guideService = new GuideService();

            MostVisitedAppointment = new ObservableCollection<Appointment>();
            FinishedAppointments = new ObservableCollection<Appointment>();

            this.ShowStatisticsCommand = new RelayCommand(Execute_ShowStatisticsCommand, CanExecute_NavigateCommand);
            this.ReportOnScheduledToursCommand = new RelayCommand(Execute_ReportOnScheduledToursCommand, CanExecute_NavigateCommand);
            this.EnableStatisticsButtonCommand = new RelayCommand(Execute_EnableStatisticsButtonCommand, CanExecute_NavigateCommand);
            this.OpenPopupCommand = new RelayCommand(Execute_OpenPopupCommand, CanExecute_NavigateCommand);
            Years = new ObservableCollection<string>();
            foreach(string year in appointmentService.GetAllYears())
                Years.Add(year);

            IsOpenedPopup = false;
            IsEnabledStatisticsButton = false;
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
