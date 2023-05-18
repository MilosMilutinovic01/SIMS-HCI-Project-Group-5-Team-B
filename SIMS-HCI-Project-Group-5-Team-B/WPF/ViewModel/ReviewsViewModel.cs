using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
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

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class ReviewsViewModel: INotifyPropertyChanged
    {
        private ObservableCollection<Card> cards;
        public bool result;
        private TourGradeService tourGradeService;
        private TourAttendanceService tourAttendanceService;
        public ObservableCollection<Card> Cards
        {
            get { return cards; }
            set
            {
                cards = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Cards)));
            }
        }

        public RelayCommandWithParams ReportCommand { get; }
        public RelayCommandWithParams MoreCommand { get; }

        public ReviewsViewModel(int userId)
        {
            TourAttendanceCSVRepository tourAttendanceCSVRepository = new TourAttendanceCSVRepository();
            TourGradeCSVRepository tourGradeCSVRepository = new TourGradeCSVRepository();

            this.tourAttendanceService = new TourAttendanceService();
            this.tourGradeService = new TourGradeService();

            Cards = new ObservableCollection<Card>();

            UserController userController = new UserController();
            KeyPointsController keyPointsController = new KeyPointsController();

            foreach (TourGrade tg in tourGradeService.GetAll())
            {
                string username = userController.getById(tg.GuideGuestId).Username;
                int keyPointArrivedId = tourAttendanceService.GetById(tg.TourAttendanceId).KeyPointGuestArrivedId;
                string keyPointName = keyPointsController.GetById(keyPointArrivedId).Name;
                Cards.Add(new Card(username, keyPointName, tg.GuideGeneralKnowledge, tg.GuideLanguageKnowledge, tg.TourFun, "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", false, false));
            }

            ReportCommand = new RelayCommandWithParams(Report);
            MoreCommand = new RelayCommandWithParams(More);
        }

        private void Report(object parameter)
        {
            result = MessageBox.Show("Are you sure you want to report selected review?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
            
            if (parameter is Card selectedCard && result)
                selectedCard.Reported = true;
        }

        private void More(object parameter)
        {
            result = MessageBox.Show("Display more?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
            //FullReviewWindow fullReviewWindow = new FullReviewWindow(Cards.GuestName);
            //fullReviewWindow.Show();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

    }
}
