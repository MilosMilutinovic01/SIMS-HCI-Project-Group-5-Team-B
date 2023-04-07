using SIMS_HCI_Project_Group_5_Team_B.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.Models
{
    public enum REQUESTSTATUS { Pending = 0, Denied, Confirmed }
    public class ReservationChangeRequest : ISerializable, IDataErrorInfo
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public REQUESTSTATUS RequestStatus { get; set; }
        public string DenialComment { get; set; }

        public ReservationChangeRequest(int id, int reservationId, Reservation reservation, DateTime start, DateTime end, REQUESTSTATUS requestStatus, string denialComment)
        {
            Id = id;
            ReservationId = reservationId;
            Reservation = reservation;
            Start = start;
            End = end;
            RequestStatus = requestStatus;
            DenialComment = denialComment;
        }

        public ReservationChangeRequest() { }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            ReservationId = int.Parse(values[1]);
            Start = DateTime.Parse(values[2]);
            End = DateTime.Parse(values[3]);
            RequestStatus = (REQUESTSTATUS)int.Parse(values[4]);
            DenialComment = values[5];

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
                DenialComment
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
