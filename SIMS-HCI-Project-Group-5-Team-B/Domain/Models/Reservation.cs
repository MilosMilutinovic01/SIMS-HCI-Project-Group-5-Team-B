using SIMS_HCI_Project_Group_5_Team_B.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.Models
{
    public class Reservation : ISerializable, IDataErrorInfo, INotifyPropertyChanged
    {

        public int Id { get; set; }

        private int accommodationId;
        public int AccommodationId
        {
            get { return accommodationId; }

            set { accommodationId = value; }

        }




        private int ownerGuestId;
        public int OwnerGuestId { get { return ownerGuestId; } set { ownerGuestId = value; } }

        public OwnerGuest OwnerGuest { get; set; }
        private Accommodation accommodation;
        public Accommodation Accommodation { get { return accommodation; } set { accommodation = value; } }
        private DateTime startDate;
        public DateTime StartDate
        {
            get
            { return startDate; }
            set
            {
                if (value != startDate)
                {
                    startDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime endDate;
        public DateTime EndDate
        {
            get
            { return endDate; }
            set
            {
                if (value != endDate)
                {
                    endDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private int reservationDays;
        public int ReservationDays
        {
            get { return reservationDays; }
            set
            {
                if (value != reservationDays)
                {
                    reservationDays = value;
                    OnPropertyChanged();
                }
            }
        }

        private int guestsNumber;
        public int GuestsNumber
        {
            get { return guestsNumber; }
            set
            {
                if (value != guestsNumber)
                {
                    guestsNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsGraded { get; set; }
        public bool IsGradedByGuest { get; set; }

        public Reservation()
        {
            OwnerGuest = new OwnerGuest();
            // ownerGuestId = 0;
            GuestsNumber = 1;
            StartDate = DateTime.Today;
            EndDate = DateTime.Today;
        }

        //Added for ligical deletition
        public bool IsDeleted { get; set; }

        public Reservation(int accomodationId, DateTime startDate, DateTime endDate, int reservationDays, int guestsNumber)
        {
            accommodationId = accomodationId;
            this.startDate = startDate;
            this.endDate = endDate;
            this.reservationDays = reservationDays;
            this.guestsNumber = guestsNumber;
            OwnerGuest = new OwnerGuest();
            ownerGuestId = 0;

            IsGraded = false;
            IsGradedByGuest = false;
            IsDeleted = false;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                accommodationId.ToString(),
                ownerGuestId.ToString(),
                startDate.ToString(),
                endDate.ToString(),
                reservationDays.ToString(),
                guestsNumber.ToString(),
                IsGraded.ToString(),
                IsGradedByGuest.ToString(),
                IsDeleted.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            accommodationId = int.Parse(values[1]);
            ownerGuestId= int.Parse(values[2]);
            startDate = DateTime.Parse(values[3]);
            endDate = DateTime.Parse(values[4]);
            reservationDays = int.Parse(values[5]);
            guestsNumber = int.Parse(values[6]);
            IsGraded = bool.Parse(values[7]);
            IsGradedByGuest = bool.Parse(values[8]);
            IsDeleted = bool.Parse(values[9]);
        }

        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                if (columnName == "StartDate")
                {
                    if (StartDate < DateTime.Today)
                    {
                        return "The reservation can not be in the past";
                    }

                }
                else if (columnName == "EndDate")
                {
                    if (StartDate.AddDays(Accommodation.MinReservationDays - 1) > EndDate)
                    {
                        if (StartDate > EndDate)
                            return "End Date must be greater than Start Date";
                        return string.Format("Minimal reservation days is {0}", Accommodation.MinReservationDays);
                    }

                }
                else if (columnName == "ReservationDays")
                {
                    if (reservationDays < Accommodation.MinReservationDays)
                    {
                        return string.Format("Minimal reservation days is {0}", Accommodation.MinReservationDays);
                    }
                }
                else if (columnName == "GuestsNumber")
                {
                    if (GuestsNumber > Accommodation.MaxGuests)
                    {
                        return "Too many guests for this accomodation";
                    }
                    else if(GuestsNumber < 1)
                    {
                        return "Minimal number of guests is 1";
                    }
                }
                return null;
            }
        }

        private readonly string[] _validatedProperties = { "EndDate", "ReservationDays", "GuestsNumber", "StartDate" };

        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool IsGradable()
        {
            return this.EndDate.AddDays(5) > DateTime.Today && this.EndDate < DateTime.Today && this.IsGradedByGuest == false;
        }

        public bool isModifiable()
        {
            return !(this.StartDate <= DateTime.Today || this.StartDate <= DateTime.Today.AddDays(this.Accommodation.NoticePeriod));
        }

        public bool IsDeletable()
        {
            return !(this.StartDate <= DateTime.Today || this.StartDate <= DateTime.Today.AddDays(this.Accommodation.NoticePeriod));
        }
    }
}
