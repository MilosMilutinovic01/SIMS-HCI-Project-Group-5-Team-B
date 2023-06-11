using PdfSharp.Pdf.IO;
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.DTO;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel.Guide
{
    public class FullReviewViewModel : ViewModel
    {
        private TourGradeService tourGradeService;
        private TourAttendanceService tourAttendanceService;
        private AppointmentService appointmentService;
        public Card card;
        public string GuestName { get; set; }
        public string TourName { get; set; }
        public string KeyPointName { get; set; }
        public string Rating { get; set; }
        public string Comment { get; set; }

        private bool isReportButtonEnabled;

        public bool IsReportButtonEnabled
        {
            get { return isReportButtonEnabled; }
            set
            {
                if (isReportButtonEnabled != value)
                {
                    isReportButtonEnabled = value;
                    OnPropertyChanged(nameof(IsReportButtonEnabled));
                }
            }
        }

        private bool firstStar;

        public bool FirstStar
        {
            get { return firstStar; }
            set
            {
                if (firstStar != value)
                {
                    firstStar = value;
                    OnPropertyChanged(nameof(FirstStar));
                }
            }
        }

        private bool secondStar;

        public bool SecondStar
        {
            get { return secondStar; }
            set
            {
                if (secondStar != value)
                {
                    secondStar = value;
                    OnPropertyChanged(nameof(SecondStar));
                }
            }
        }

        private bool thirdStar;

        public bool ThirdStar
        {
            get { return thirdStar; }
            set
            {
                if (thirdStar != value)
                {
                    thirdStar = value;
                    OnPropertyChanged(nameof(ThirdStar));
                }
            }
        }

        private bool fourthStar;

        public bool FourthStar
        {
            get { return fourthStar; }
            set
            {
                if (fourthStar != value)
                {
                    fourthStar = value;
                    OnPropertyChanged(nameof(FourthStar));
                }
            }
        }

        private bool fifthStar;

        public bool FifthStar
        {
            get { return fifthStar; }
            set
            {
                if (fifthStar != value)
                {
                    fifthStar = value;
                    OnPropertyChanged(nameof(FifthStar));
                }
            }
        }
        public RelayCommandWithParams ReportCommand { get; }
        public RelayCommandWithParams CloseCommand { get; }
        private bool CanExecute_NavigateCommand(object parameter)
        {
            return true;
        }
        private void Execute_ReportCommand(object parameter)
        {
            bool result = MessageBox.Show("Are you sure you want to report selected review?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;

            if (card is Card selectedCard && result)
            {
                selectedCard.Reported = true;
                selectedCard.Valid = false;
                TourGrade tg = tourGradeService.getById(selectedCard.TourGradeId);
                tg.Valid = false;
                tourGradeService.Update(tg);
            }
        }
        private void Execute_CloseCommand(object parameter)
        {
        Window window = System.Windows.Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            if (window != null)
            {
                window.Close();
            }
        }

        private void RatingStars(double rating)
        {
            if (rating < 5)
                FifthStar = false;
            if (rating < 4)
                FourthStar = false;
            if (rating < 3)
                ThirdStar = false;
            if (rating < 2)
                SecondStar = false;
            if (rating < 1)
                FirstStar = false;
        }
        public FullReviewViewModel(Card card) 
        {
            this.tourAttendanceService = new TourAttendanceService();
            this.tourGradeService = new TourGradeService();
            this.appointmentService = new AppointmentService();
            this.card = card;

            this.GuestName = "Guest name: " + card.GuestName;
            this.TourName = "Tour name: " + card.TourName;
            this.KeyPointName = "Key point name: " + card.KeyPointName;
            double rating = (double)(card.TourFun + card.GeneralKnowledge + card.LanguageKnowledge) / 3;
            this.Rating = "Rating: " + String.Format("{0:0.00}", rating);
            this.Comment = "Full comment: " + card.Comment;

            this.ReportCommand = new RelayCommandWithParams(Execute_ReportCommand, CanExecute_NavigateCommand);
            this.CloseCommand = new RelayCommandWithParams(Execute_CloseCommand, CanExecute_NavigateCommand);

            IsReportButtonEnabled = card.Valid;
            FifthStar = true;
            ThirdStar = true;
            FourthStar = true;
            FirstStar = true;
            SecondStar = true;
            RatingStars(rating);
        }
    }
}
