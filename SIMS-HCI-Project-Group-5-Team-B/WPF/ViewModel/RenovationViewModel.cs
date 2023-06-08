using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.ServiceInterfaces;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class RenovationViewModel
    {
        public ObservableCollection<RenovationGridView> FutureRenovations { get; set; }
        public ObservableCollection<Renovation> PastRenovations { get; set; }
        public RenovationGridView SelectedRenovationGridView { get; set; }
        public Renovation NewRenovation { get; set; }
        public ObservableCollection<RenovationProposalDates> RenovationProposalDates { get; set; }
        public RenovationProposalDates SelectedDate { get; set; }
        public string SelectedAccommodationName { get; set; }
        public List<string> AccommodationNames { get; set; }
        private IRenovationService renovationService;
        private ReservationService reservationService;
        private AccommodationService accommodationService;
        private Owner Owner { get; set; }
        public RelayCommand CallOffWindowCommand { get; }
        public RelayCommand CallOffCommand { get; }
        public RelayCommand CancelCommand { get; }
        public RelayCommand ScheduleWindowCommand { get; }
        public RelayCommand SearchAvailableDatesCommand { get; }
        
        public RelayCommand ScheduleRenovationCommand { get; }
        public RenovationViewModel(IRenovationService renovationService,ReservationService reservationService, Owner owner,AccommodationService accommodationService)
        {
            this.renovationService = renovationService;
            this.reservationService = reservationService;
            this.accommodationService = accommodationService;
            this.Owner = owner;
            AccommodationNames = new List<string>();
            NewRenovation = new Renovation();
            RenovationProposalDates = new ObservableCollection<RenovationProposalDates>();
            SelectedDate = new RenovationProposalDates(DateTime.MinValue, DateTime.MinValue);
            NewRenovation.StartDate = DateTime.Today;
            NewRenovation.EndDate = DateTime.Today;
            FutureRenovations = new ObservableCollection<RenovationGridView>(renovationService.GetFutureRenovationsView(owner.Id));
            PastRenovations = new ObservableCollection<Renovation>(renovationService.GetPastRenovations(owner.Id));
            GetAccommodationNames(owner.Id);
            CallOffWindowCommand = new RelayCommand(CallOffWindow_Execute, CanCallOffExecute);
            CallOffCommand = new RelayCommand(CallOffExecute, CanCallOffExecute);
            CancelCommand = new RelayCommand(CancelExecute, CanExecute);
            ScheduleWindowCommand = new RelayCommand(ScheduleWindow_Execute, CanExecute);
            SearchAvailableDatesCommand = new RelayCommand(SeachAvailableDatesExecute, CanExecute);
            ScheduleRenovationCommand = new RelayCommand(ScheduleRenovationExecute, CanExecute);
        }

        public void ScheduleWindow_Execute()
        {
            ScheduleRenovationForm scheduleRenovationForm = new ScheduleRenovationForm(renovationService,reservationService, accommodationService, Owner, FutureRenovations);
            scheduleRenovationForm.Show();
        }
        
        public void CancelExecute()
        {
            App.Current.Windows[4].Close();
        }

        public bool CanExecute()
        {
            return true;
        }

        public bool CanCallOffExecute()
        {
            return true;
        }


        public void CallOffWindow_Execute()
        {
            if(SelectedRenovationGridView != null)
            {
                CallOffRenovationWindow callOffRenovationWindow = new CallOffRenovationWindow(renovationService,reservationService,Owner,SelectedRenovationGridView,FutureRenovations,accommodationService);
                callOffRenovationWindow.Show();
            }
        }

        public void GetAccommodationNames(int ownerId)
        {
            foreach (Accommodation accommodation in accommodationService.GetUndeleted())
            {
                if (accommodation.Owner.Id == ownerId)
                {
                    AccommodationNames.Add(accommodation.Name);
                }
            }
        }


        public void CallOffExecute()
        {
            if (SelectedRenovationGridView != null && SelectedRenovationGridView.CanBeCalledOff)
            {
                
               SelectedRenovationGridView.Renovation.IsDeleted = true;
               renovationService.Update(SelectedRenovationGridView.Renovation);
               FutureRenovations.Remove(SelectedRenovationGridView);
                if (Properties.Settings.Default.currentLanguage == "en-US")
                {
                    MessageBox.Show("Renovation was succesfully called off!");
                    CancelExecute();
                }
                else
                {
                    MessageBox.Show("Renoviranje je uspesno otkazano!");
                    CancelExecute();
                }

            }
        }

        public void ScheduleRenovationExecute()
        {
            
                if (NewRenovation.IsValid)
                {
                    NewRenovation.AccommodationId = accommodationService.GetIdByName(SelectedAccommodationName, Owner);
                    NewRenovation.Accommodation = accommodationService.GetById(NewRenovation.AccommodationId);
                    NewRenovation.StartDate = SelectedDate.Start;
                    NewRenovation.EndDate = SelectedDate.End;
                    renovationService.Save(NewRenovation);
                    
                    if (DateTime.Today.AddDays(5) < NewRenovation.StartDate)
                    {
                        FutureRenovations.Add(new RenovationGridView(NewRenovation, true));
                    }
                    else
                    {
                        FutureRenovations.Add(new RenovationGridView(NewRenovation, false));
                    }
                    if (Properties.Settings.Default.currentLanguage == "en-US")
                    {
                        MessageBox.Show("Renovation succesfully scheduled!");
                    }
                    else
                    {
                        MessageBox.Show("Renoviranje uspesno zakazano!");
                    }


                }
                else
                {
                    if (Properties.Settings.Default.currentLanguage == "en-US")
                    {
                        MessageBox.Show("Renovation can't be scheduled, because fileds are not valid");
                    }
                    else
                    {
                        MessageBox.Show("Renoviranje ne moze biti zakazano, jer polja nisu validna");
                    }
                }
            
            
        }

        public void SeachAvailableDatesExecute()
        {
            if (NewRenovation.IsValidForSearch)
            {
                NewRenovation.AccommodationId = accommodationService.GetIdByName(SelectedAccommodationName, Owner);
                NewRenovation.Accommodation = accommodationService.GetById(NewRenovation.AccommodationId);
                RenovationProposalDates.Clear();
                List<RenovationProposalDates> list = reservationService.GetRenovationProposalDatesInTimeSpan(NewRenovation.Accommodation, NewRenovation.StartDate, NewRenovation.EndDate, NewRenovation.RenovationDays);
                if (list != null)
                {
                    foreach (RenovationProposalDates item in list)
                    {
                        RenovationProposalDates.Add(item);
                    }
                }
            }
            else
            {
               
                    if (Properties.Settings.Default.currentLanguage == "en-US")
                    {
                        MessageBox.Show("Search can not be preformed because data is not valid!");
                    }
                    else
                    {
                        MessageBox.Show("Pretraga ne moze biti izvrsena jer vrednosti nisu validne");
                    }
                
                //MessageBox.Show("Search can not be preformed because data is not valid!");
            }
        }
    }
}
