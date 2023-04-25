using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.DTO;
using System;
using System.Collections.ObjectModel;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class OwnerGuestGradesViewModel
    {
        private OwnerGuestGradeService ownerGuestGradeService;
        private AccommodationService accommodationService;
        private LocationController locationController;
        private ReservationService reservationService;
        private OwnerService ownerService;

        public ObservableCollection<OwnerGuestGradesDTO> GradeDTOs { get; set; }
        public double AverageGrade { get; set; }
        public int GradeCount { get; set; } = 0;
        public OwnerGuestGradesDTO SelectedGrade { get; set; }
        public OwnerGuestGradesViewModel(int ownerGuestId)
        {
            locationController = new LocationController();
            ownerService = new OwnerService();
            accommodationService = new AccommodationService(locationController, ownerService);
            reservationService = new ReservationService(accommodationService);
            ownerGuestGradeService = new OwnerGuestGradeService(reservationService);

            GradeDTOs = new ObservableCollection<OwnerGuestGradesDTO>(ownerGuestGradeService.GetOwnerGuestsGradesDTO(ownerGuestId));
            AverageGrade = Math.Round(ownerGuestGradeService.GetAverageGrade(ownerGuestId),2);
            GradeCount = ownerGuestGradeService.GetGradesCount(ownerGuestId);
        }
    }
}
