using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
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

        public ReviewsViewModel()
        {
            Cards = new ObservableCollection<Card>();

            int i = 0;
            Cards.Add(new Card(i++, "Uros Nikolovski", "Tura 1", "Ledinacko jezero", 5, 5, 5, "Svaka cast gosn!", false, false));
            Cards.Add(new Card(i++, "Jelena Kovac", "Tura 2", "Popovicko jezero", 3, 4, 5, "fdsaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa\naaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa!", false, false));
            Cards.Add(new Card(i++, "Nina Kuzminac", "Tura 3", "Knicko jezero", 4, 4, 3, "sdasaf!", false, false)); //get data from file

            ReportCommand = new RelayCommandWithParams(Report);
        }

        private void Report(object parameter)
        {
            if (parameter is Card selectedCard)
                selectedCard.Reported = true;
            result = MessageBox.Show("Are you sure you want to report selected review?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

    }
}
