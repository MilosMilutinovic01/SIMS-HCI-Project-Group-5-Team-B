using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel;

namespace SIMS_HCI_Project_Group_5_Team_B.Application.UseCases
{
    public class YearlyAccommodationStatisticsService
    {
        private IReservationRepository reservationRepository;
        private IRenovationRequestRepository renovationRequestRepository;
        private IReservationChangeRequestRepository reservationChangeRequestRepository;


        public YearlyAccommodationStatisticsService()
        {
            this.reservationRepository = Injector.Injector.CreateInstance<IReservationRepository>();
            this.renovationRequestRepository = Injector.Injector.CreateInstance<IRenovationRequestRepository>();
            this.reservationChangeRequestRepository = Injector.Injector.CreateInstance<IReservationChangeRequestRepository>();

        }

        public List<YearlyAccommodationStatistics> GetYearlyStatistics(int accommodationId)
        {
            List<Reservation> undeletedReservations = reservationRepository.GetAll().Where(r => r.Accommodation.Id == accommodationId && r.IsDeleted == false).ToList(); //dobavlja sve rezervacije za zeljeni smestaj koje nisu obrisane
            List<RenovationRequest>  renovationRequests = renovationRequestRepository.GetAll().Where(r => r.Reservation.AccommodationId == accommodationId).ToList(); //dobavlja sve preporuke renoviranja za zeljeni smestaj 
            List<ReservationChangeRequest> reservationChangeRequests = reservationChangeRequestRepository.GetAll().Where(r => r.Reservation.Accommodation.Id == accommodationId && r.RequestStatus == REQUESTSTATUS.Confirmed).ToList(); //dobavlja sve potvrdjenje zahteve za zeljeni smestaj
            List<Reservation> deletedReservations = reservationRepository.GetAll().Where(r => r.Accommodation.Id == accommodationId && r.IsDeleted == true).ToList(); //ako je zahtev potvrdjen znaci da je doslo do pomeranja

            //dobavlja sve razlicite godine za koje se desile rezervacije ali za poseban smestaj
            List<int> years = new List<int>();
            years.AddRange(undeletedReservations.Select(r => r.StartDate.Year).Distinct());//
            years.AddRange(undeletedReservations.Select(r => r.EndDate.Year).Distinct());// ako rezervacija pocne u 2023 ,a zavrsi se u 2024, dobavice 2024 zbog racunnja zauzetosti
            years.AddRange(deletedReservations.Select(r => r.StartDate.Year).Distinct());
            years.AddRange(reservationChangeRequests.Select(r => r.Start.Year).Distinct());
            years = years.Distinct().OrderBy(y => y).ToList();//redja ih i one se ne ponavljaju
            
            List<YearlyAccommodationStatistics> yearlyStatistics = new List<YearlyAccommodationStatistics>();
            foreach (int year in years)
            {
                yearlyStatistics.Add(GetYearlyAccommodationStatistics(undeletedReservations, deletedReservations, reservationChangeRequests, renovationRequests, year));
            }

            return yearlyStatistics;
        }


        public YearlyAccommodationStatistics GetYearlyAccommodationStatistics(List<Reservation> undeletedReservations, List<Reservation> deletedReservations, List<ReservationChangeRequest> reservationChangeRequests, List<RenovationRequest> renovationRequests, int year)
        {
            YearlyAccommodationStatistics yearlyAccommodationStatistics = new YearlyAccommodationStatistics();
            yearlyAccommodationStatistics.Year = year;
            yearlyAccommodationStatistics.NumberOfReservations = undeletedReservations.Count(r => r.StartDate.Year == year);
            yearlyAccommodationStatistics.NumberOfCancelledReservations = deletedReservations.Count(r => r.StartDate.Year == year && r.IsDeleted == true);
            yearlyAccommodationStatistics.NumberOfChangedReservationDates = reservationChangeRequests.Count(r => r.Start.Year == year);
            yearlyAccommodationStatistics.NumberOfRenovationRequests = renovationRequests.Count(r => r.Reservation.StartDate.Year == year);
            DateTime firstDay = new DateTime(year, 1, 1);
            DateTime lastDay = new DateTime(year, 12, 31);
            yearlyAccommodationStatistics.Busyness = GetNumberOfReservedDaysInYear(undeletedReservations, year) / ((lastDay - firstDay).TotalDays + 1);
            yearlyAccommodationStatistics.IsBusiest = false;

            return yearlyAccommodationStatistics;
        }


        //3 slucaja za racunjanje zauzetih dana u godini
        public double GetNumberOfReservedDaysInYear(List<Reservation> reservations, int year)
        {
            double sum = 0;
            foreach (Reservation reservation in reservations)
            {
                //kada se cela rezervacija nalazi u istoj godini
                if (reservation.EndDate.Year == year && reservation.StartDate.Year == year)
                {
                    sum = sum + (reservation.EndDate - reservation.StartDate).TotalDays + 1;
                }
                //kada rezervacija zavrsava u ovoj godini , a pocinje u prosloj
                else if (reservation.EndDate.Year == year && reservation.StartDate.Year != year)
                {
                    DateTime firstDay = new DateTime(year, 1, 1);
                    sum = sum + (reservation.EndDate - firstDay).TotalDays + 1;
                }
                //kada rezervacija pocinje u ovoj godini, a zavrsava se u novoj
                else if (reservation.EndDate.Year != year && reservation.StartDate.Year == year)
                {
                    DateTime lastDay = new DateTime(reservation.StartDate.Year, 12, 31);
                    sum = sum + (lastDay - reservation.StartDate).TotalDays + 1;
                }
            }

            return sum;
        }

        public void MarkBusiest(ObservableCollection<YearlyAccommodationStatistics> yearlyStatistics)
        {
            //prolazi se kroz kroz sve godine i nalazi se vrednost najvece zauzetosti
            double maximumBusyness = 0;
            foreach (YearlyAccommodationStatistics yearlyAccommodationStatistics in yearlyStatistics)
            {
                if (yearlyAccommodationStatistics.Busyness > maximumBusyness)
                {
                    maximumBusyness = yearlyAccommodationStatistics.Busyness;
                }
            }
            //prolazi se kroz sve statistike i uporedjuje se zauzetost sa prethodno nadjenom njavecom zauzestosti,ona koja se poklapa ta se oznacava da je najzauzetija
            foreach (YearlyAccommodationStatistics yearlyAccommodationStatistics in yearlyStatistics)
            {
                if (yearlyAccommodationStatistics.Busyness == maximumBusyness)
                {
                    yearlyAccommodationStatistics.IsBusiest = true;
                }
            }

        }


        public double CalculateAverageNumberOfReservationsForAccommodations(List<Accommodation> accommodationsOnLocation)
        {
            double averageNumberOfReservations = 0;
            foreach (Accommodation accommodation in accommodationsOnLocation)
            {
                int numberOfReservationForAccommodation = 0;
                List<YearlyAccommodationStatistics> yearlyAccommodationStatistics = new List<YearlyAccommodationStatistics>(GetYearlyStatistics(accommodation.Id));
                if (yearlyAccommodationStatistics.Count() != 0)
                {
                    foreach (YearlyAccommodationStatistics yearlyAccommodationStatistic in yearlyAccommodationStatistics)
                    {
                        numberOfReservationForAccommodation = numberOfReservationForAccommodation + yearlyAccommodationStatistic.NumberOfReservations;
                    }
                    int numberOfYears = yearlyAccommodationStatistics.Count();
                    averageNumberOfReservations = averageNumberOfReservations + (double)numberOfReservationForAccommodation / numberOfYears;
                }
            }
            return averageNumberOfReservations;
        }

        public double CalculateAverageBusynessForAccommodations(List<Accommodation> accommodationsOnLocation)
        {
            double averageBusyness = 0;
            foreach (Accommodation accommodation in accommodationsOnLocation)
            {
                double averageBusynessForAccommodation = 0;
                List<YearlyAccommodationStatistics> yearlyAccommodationStatistics = new List<YearlyAccommodationStatistics>(GetYearlyStatistics(accommodation.Id));
                if (yearlyAccommodationStatistics.Count() != 0)
                {
                    foreach (YearlyAccommodationStatistics yearlyAccommodationStatistic in yearlyAccommodationStatistics)
                    {
                        averageBusynessForAccommodation = averageBusynessForAccommodation + yearlyAccommodationStatistic.Busyness;
                    }
                    int numberOfYears = yearlyAccommodationStatistics.Count();
                    averageBusyness = averageBusyness + (double)averageBusynessForAccommodation / numberOfYears;
                }
            }
            return averageBusyness;
        }



    }
}
