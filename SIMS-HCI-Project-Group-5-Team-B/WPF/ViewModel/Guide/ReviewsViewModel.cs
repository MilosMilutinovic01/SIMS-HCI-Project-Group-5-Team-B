using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using SIMS_HCI_Project_Group_5_Team_B.DTO;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View.Guide;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Effects;
using System.Xml.Linq;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class ReviewsViewModel: ViewModel, INotifyPropertyChanged
    {
        private ObservableCollection<Card> cards;
        public bool result;
        private TourGradeService tourGradeService;
        private TourAttendanceService tourAttendanceService;
        private AppointmentService appointmentService;
        public ObservableCollection<Card> Cards
        {
            get { return cards; }
            set
            {
                cards = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Cards)));
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

        public RelayCommandWithParams ReportCommand { get; }
        public RelayCommandWithParams MoreCommand { get; }
        public RelayCommand OpenPopupCommand { get; set; }
        private bool CanExecute_NavigateCommand()
        {
            return true;
        }
        private void Execute_OpenPopupCommand()
        {
            IsOpenedPopup = !IsOpenedPopup;
        }
        public ReviewsViewModel(int userId)
        {
            this.tourAttendanceService = new TourAttendanceService();
            this.tourGradeService = new TourGradeService();
            this.appointmentService = new AppointmentService();

            Cards = new ObservableCollection<Card>();

            UserController userController = new UserController();
            KeyPointsController keyPointsController = new KeyPointsController();

            foreach (TourGrade tg in tourGradeService.GetAll())
            {
                string username = userController.getById(tg.GuideGuestId).Username;
                int keyPointArrivedId = tourAttendanceService.GetById(tg.TourAttendanceId).KeyPointGuestArrivedId;
                string keyPointName = keyPointsController.GetById(keyPointArrivedId).Name;
                string tourName = appointmentService.getById(tourAttendanceService.GetById(tourGradeService.getById(tg.Id).TourAttendanceId).AppointmentId).Tour.Name;
                Cards.Add(new Card(username, keyPointName, tg.GuideGeneralKnowledge, tg.GuideLanguageKnowledge, tg.TourFun, "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", tg.Valid, false, tourName, tg.Id));
            }

            this.ReportCommand = new RelayCommandWithParams(Report);
            this.MoreCommand = new RelayCommandWithParams(More);
            this.OpenPopupCommand = new RelayCommand(Execute_OpenPopupCommand, CanExecute_NavigateCommand);

            IsOpenedPopup = false;
        }

        private void Report(object parameter)
        {
            result = MessageBox.Show("Are you sure you want to report selected review?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
            
            if (parameter is Card selectedCard && result)
            {
                selectedCard.Reported = true;
                selectedCard.Valid = false;
                TourGrade tg = tourGradeService.getById(selectedCard.TourGradeId);
                tg.Valid = false;
                tourGradeService.Update(tg);
            }
            
            cards.Clear();
            UserController userController = new UserController();
            KeyPointsController keyPointsController = new KeyPointsController();
            
            foreach (TourGrade tg in tourGradeService.GetAll())
            {
                string username = userController.getById(tg.GuideGuestId).Username;
                int keyPointArrivedId = tourAttendanceService.GetById(tg.TourAttendanceId).KeyPointGuestArrivedId;
                string keyPointName = keyPointsController.GetById(keyPointArrivedId).Name;
                string tourName = appointmentService.getById(tourAttendanceService.GetById(tourGradeService.getById(tg.Id).TourAttendanceId).AppointmentId).Tour.Name;
                Cards.Add(new Card(username, keyPointName, tg.GuideGeneralKnowledge, tg.GuideLanguageKnowledge, tg.TourFun, "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", tg.Valid, false, tourName, tg.Id));
            }
        }

        private void More(object parameter)
        {
            //result = MessageBox.Show("Display more?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
            if (parameter is Card selectedCard)
            {
                Window window = System.Windows.Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                if (window != null)
                {
                    window.Effect = new BlurEffect();
                }
                FullReviewWindow fullReviewWindow = new FullReviewWindow(selectedCard);
                fullReviewWindow.ShowDialog();
                window.Effect = null;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

    }
}
