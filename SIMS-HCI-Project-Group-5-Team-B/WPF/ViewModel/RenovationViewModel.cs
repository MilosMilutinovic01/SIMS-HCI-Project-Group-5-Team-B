using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.View;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using System.Collections.ObjectModel;
using System.Windows;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class RenovationViewModel
    {
        public ObservableCollection<RenovationGridView> renovationGridViews { get; set; }
        public RenovationGridView SelectedRenovationGridView { get; set; }
        public Renovation NewRenovation { get; set; }
        public ObservableCollection<RenovationRecommendation> RenovationRecommendations { get; set; }
        public RenovationRecommendation SelectedDate { get; set; }

        private RenovationService renovationService;
        private ReservationService reservationService;
        

        public RenovationViewModel(RenovationService renovationService,ReservationService reservationService)
        {
            this.renovationService = renovationService;
            this.reservationService = reservationService;
            NewRenovation = new Renovation();
            RenovationRecommendations = new ObservableCollection<RenovationRecommendation>();
            SelectedDate = new RenovationRecommendation(DateTime.MinValue, DateTime.MinValue);
            NewRenovation.StartDate = DateTime.Now;
            NewRenovation.EndDate = DateTime.Now;  
        }

        public void CallOff()
        {
            if (SelectedRenovationGridView != null)
            {

            }
        }

        public void CreateRenovation()
        {
            if (NewRenovation.IsValid)
            {
                renovationService.Save(NewRenovation);
                MessageBox.Show("Renovation scheduled!");
            }
            else
            {
                MessageBox.Show("Renovation can not be scheduled\nBecause data is not valid!");
            }
        }

        public void Schedule()
        {
            if(SelectedDate != null)
            {
                NewRenovation.StartDate = SelectedDate.Start;
                NewRenovation.EndDate = SelectedDate.End;

                RenovationDescriptionWindow renovationDescriptionWindow = new RenovationDescriptionWindow(NewRenovation,renovationService,reservationService);
                renovationDescriptionWindow.Show();
            }
        }

        public void SeachAvailableDates()
        {
            if (NewRenovation.IsValid)
            {
                RenovationRecommendations.Clear();
                List<RenovationRecommendation> list = reservationService.GetRenovationRecommendationsInTimeSpan(NewRenovation.Accommodation, NewRenovation.StartDate, NewRenovation.EndDate, NewRenovation.RenovationDays);
                if (list != null)
                {
                    foreach (RenovationRecommendation item in list)
                    {
                        RenovationRecommendations.Add(item);
                    }
                }


            }
            else
            {
                MessageBox.Show("Search can not be preformed because data is not valid!");
            }
        }
    }
}
