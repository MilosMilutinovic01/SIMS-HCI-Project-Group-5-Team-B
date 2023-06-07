using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Notifications;
using SIMS_HCI_Project_Group_5_Team_B.View;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View.OwnerGuest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Threading;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class OwnerGuestWindowViewModel
    {
        private AccommodationService accommodationService;
        private LocationController locationController;
        private ReservationService reservationService;
        private OwnerService ownerService;
        private OwnerAccommodationGradeSevice ownerAccommodationGradeService;
        private SuperOwnerService superOwnerService;
        private OwnerGuestService ownerGuestService;
        private OwnerGuest activeOwnerGuest;
        private ReservationChangeRequestService reservationChangeRequestService;
        private SuperOwnerGuestTitleService superOwnerGuestTitleService;
        private string username;
        private Frame frame;

        public OwnerGuestWindowViewModel(string username, Frame frame)
        {
            this.username = username;
            locationController = new LocationController();
            ownerService = new OwnerService();
            accommodationService = new AccommodationService(locationController, ownerService);
            reservationService = new ReservationService();
            ownerAccommodationGradeService = new OwnerAccommodationGradeSevice(reservationService);
            superOwnerService = new SuperOwnerService(ownerAccommodationGradeService, accommodationService);
            ownerGuestService = new OwnerGuestService();
            reservationChangeRequestService = new ReservationChangeRequestService();
            superOwnerGuestTitleService = new SuperOwnerGuestTitleService();
            activeOwnerGuest = ownerGuestService.GetByUsername(username);
            this.frame  = frame;


            //check for updates for superOwnerGuestTitle
            superOwnerGuestTitleService.CheckForNewTitles();
            
        }

        public void ShowAccommodation()
        {
            frame.Content = new AccommodationsPage(activeOwnerGuest.Id);
        }

        public void ShowReservations()
        {
            frame.Content = new ReservationsPage(reservationService, ownerAccommodationGradeService, superOwnerService, ownerService, activeOwnerGuest.Id, reservationChangeRequestService);
        }

        public void ShowNotifications()
        {
            frame.Content = new NotificationsPage(activeOwnerGuest.Id);
        }

        public void ShowGrades()
        {
            frame.Content = new GradesPage(activeOwnerGuest.Id);
        }

        public void ShowForums()
        {
            frame.Content = new ForumsPage(activeOwnerGuest.Id);
        }

        public void ShowAnywhereAnytime()
        {
            frame.Content = new AnywhereAnytimePage(accommodationService, reservationService);
        }
    }
}
