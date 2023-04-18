using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.DTO;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
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

        public ReviewsViewModel(TourGradeService tourGradeService, int userId, TourAttendanceService tourAttendanceService)
        {
            Cards = new ObservableCollection<Card>();

            UserController userController = new UserController();
            KeyPointsController keyPointsController = new KeyPointsController();

            foreach (TourGrade tg in tourGradeService.GetAll())
            {
                string username = userController.getById(tg.GuideGuestId).Username;
                int keyPointArrivedId = tourAttendanceService.GetById(tg.TourAttendanceId).KeyPointGuestArrivedId;
                string keyPointName = keyPointsController.GetById(keyPointArrivedId).Name;
                Cards.Add(new Card(username, keyPointName, tg.GuideGeneralKnowledge, tg.GuideLanguageKnowledge, tg.TourFun, tg.Comment, false, false));
            }

            ReportCommand = new RelayCommandWithParams(Report);
        }

        private void Report(object parameter)
        {
            result = MessageBox.Show("Are you sure you want to report selected review?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
            
            if (parameter is Card selectedCard && result)
                selectedCard.Reported = true;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

    }
}
