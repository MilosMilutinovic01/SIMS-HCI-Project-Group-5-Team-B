using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using System.Collections.ObjectModel;
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using System.Globalization;


namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class YearlyAccommodationStatisticsViewModel
    {
        public ObservableCollection<YearlyAccommodationStatistics> YearlyStatistics { get; set; }
        private YearlyAccommodationStatisticsService yearlyAccommodationStatisticsService;
        public YearlyAccommodationStatistics SelectedYearlyAccommodationStatistics { get; set; }

        public YearlyAccommodationStatisticsViewModel(YearlyAccommodationStatisticsService yearlyAccommodationStatisticsService)
        {
            this.yearlyAccommodationStatisticsService = yearlyAccommodationStatisticsService;
            this.YearlyStatistics = new ObservableCollection<YearlyAccommodationStatistics>();
        }

        /*public ObservableCollection<YearlyAccommodationStatistics> GetYearlyAccommodationStatistics(int accommodationId)
        {
            /*List<Reservation> reservations = reservationService.GetAll();
            List<RenovationRequest> renovationRequests = renovationRequestService.GetAll();
            List<ReservationChangeRequest> reservationChangeRequests = reservationChangeRequestService.GetAll();

            List<Reservation> undeletedReservations = reservations.Where(r => r.Accommodation.Id == accommodationId && r.IsDeleted == false).ToList(); //dobavlja sve rezervacije za zeljeni smestaj koje nisu obrisane
            renovationRequests = renovationRequests.Where(r => r.Reservation.AccommodationId == accommodationId).ToList(); //dobavlja sve preporuke renoviranja za zeljeni smestaj 
            reservationChangeRequests = reservationChangeRequests.Where(r => r.Reservation.Accommodation.Id == accommodationId && r.RequestStatus == REQUESTSTATUS.Confirmed).ToList(); //dobavlja sve potvrdjenje zahteve za zeljeni smestaj
            List<Reservation> deletedReservations = reservations.Where(r => r.Accommodation.Id == accommodationId && r.IsDeleted == true).ToList(); //ako je zahtev potvrdjen znaci da je doslo do pomeranja
            

            //dobavlja sve razlicite godine za koje se desile rezervacije ili renoviranja ali za poseban smestaj
            List<int> years = new List<int>();
            years.AddRange(undeletedReservations.Select(r => r.StartDate.Year).Distinct());
            //*****OVO OBRISATI MENI JE GLUPO!!!!!!!!!!!!!!!!!!!
            years.AddRange(undeletedReservations.Select(r => r.EndDate.Year).Distinct());
            //******
            years.AddRange(deletedReservations.Select(r => r.StartDate.Year).Distinct());
            years.AddRange(reservationChangeRequests.Select(r => r.Start.Year).Distinct());
            years = years.Distinct().OrderBy(y => y).ToList();

            ObservableCollection<YearlyAccommodationStatistics> yearlyStatistics = new ObservableCollection<YearlyAccommodationStatistics>();
            foreach (int year in years)
            {
                YearlyAccommodationStatistics yearlyAccommodationStatistics = new YearlyAccommodationStatistics();
                yearlyAccommodationStatistics.Year = year;

                yearlyAccommodationStatistics.NumberReservations = undeletedReservations.Count(r => r.StartDate.Year == year);

                yearlyAccommodationStatistics.NumberOfCancelledReservations = deletedReservations.Count(r => r.StartDate.Year == year && r.IsDeleted == true);

                //ovde je pre bilo r => r.Reservation.StartDate.Year == year
                yearlyAccommodationStatistics.NumberOfChangedReservationDates = reservationChangeRequests.Count(r => r.Start.Year == year);

                yearlyAccommodationStatistics.NumberRenovationRequests = renovationRequests.Count(r => r.Reservation.StartDate.Year == year);


                DateTime firstDay = new DateTime(year, 1, 1);
                DateTime lastDay = new DateTime(year, 12, 31);
                yearlyAccommodationStatistics.Busyness = GetNumberOfReservedDaysInYear(undeletedReservations,year)/((lastDay - firstDay).TotalDays + 1);
                yearlyAccommodationStatistics.IsBusiest = false;

                yearlyStatistics.Add(yearlyAccommodationStatistics);
            }

            return yearlyStatistics;

            YearlyStatistics.Clear();
            foreach(YearlyAccommodationStatistics yearlyAccommodationStatistics in yearlyAccommodationStatisticsService.GetYearlyAccommodationStatistics(accommodationId))
            {
                YearlyStatistics.Add(yearlyAccommodationStatistics);
            }
            return YearlyStatistics;

        }*/


        public void GetYearlyAccommodationStatistics(int accommodationId)
        {
            YearlyStatistics.Clear();
            foreach (YearlyAccommodationStatistics yearlyAccommodationStatistics in yearlyAccommodationStatisticsService.GetYearlyAccommodationStatistics(accommodationId))
            {
                YearlyStatistics.Add(yearlyAccommodationStatistics);
            }
        }

        public void MarkBusiest(ObservableCollection<YearlyAccommodationStatistics> yearlyStatistics)
        {
            yearlyAccommodationStatisticsService.MarkBusiest(yearlyStatistics);
        }




    }
}
