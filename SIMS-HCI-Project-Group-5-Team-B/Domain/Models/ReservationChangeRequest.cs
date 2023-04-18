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
    public enum REQUESTSTATUS { Pending = 0, Denied, Confirmed}
    public class ReservationChangeRequest : ISerializable, IDataErrorInfo, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public REQUESTSTATUS RequestStatus { get; set; }

        public string denialComment;
        public string DenialComment
        {
            get
            {
                return denialComment;
            }
            set
            {
                if(denialComment != value)
                {
                    denialComment = value;
                    OnPropertyChanged();
                    NotifyPropertyChanged(nameof(DenialComment));
                }
            }
        }

        public string isAvailable;
        public string IsAvailable
        {
            get
            {
                return isAvailable;
            }
            set
            {
                if(isAvailable != value)
                {
                    isAvailable = value;
                    OnPropertyChanged();
                    NotifyPropertyChanged(nameof(IsAvailable));
                }
            }
        
        }


        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }


        

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ReservationChangeRequest( Reservation reservation)
        {
          
            ReservationId = reservation.Id;
            Reservation = reservation;
            Start = reservation.StartDate;
            End = reservation.EndDate;
            RequestStatus = REQUESTSTATUS.Pending;
            DenialComment = "";
            IsAvailable = "";
            
        }

        public ReservationChangeRequest() { }

        public void FromCSV(string[] values)
        {
            var temp = REQUESTSTATUS.Pending;
            if (values[4] == "Denied")
                temp = REQUESTSTATUS.Denied;
            else if (values[4] == "Confirmed")
                temp = REQUESTSTATUS.Confirmed;
                Id = int.Parse(values[0]);
            ReservationId = int.Parse(values[1]);
            Start = DateTime.Parse(values[2]);
            End = DateTime.Parse(values[3]);
            RequestStatus = temp;
            denialComment = values[5];
            isAvailable = values[6];


        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                ReservationId.ToString(),
                Start.ToString(),
                End.ToString(),
                RequestStatus.ToString(),
                DenialComment,
                IsAvailable
            };
            return csvValues;
        }

        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                if (columnName == "Start")
                {
                    if (Start < DateTime.Today)
                    {
                        return "The reservation can not be in the past";
                    }

                }
                else if (columnName == "End")
                {
                    if (Start.AddDays(Reservation.Accommodation.MinReservationDays - 1) > End)
                    {
                        if (Start > End)
                            return "End Date must be greater than Start Date";
                        return string.Format("Minimal reservation days is {0}", Reservation.Accommodation.MinReservationDays);
                    }

                }

                return null;
            }
        }

        private readonly string[] _validatedProperties = { "Start", "End" };

        public event PropertyChangedEventHandler? PropertyChanged;

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

    }
}
