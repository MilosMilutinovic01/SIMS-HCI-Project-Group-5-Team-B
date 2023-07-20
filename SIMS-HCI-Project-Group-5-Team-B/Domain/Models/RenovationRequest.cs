using SIMS_HCI_Project_Group_5_Team_B.Serializer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.Models
{
    public enum RENOVATIONLEVEL {Level1=0, Level2, Level3, Level4, Level5 };
    public class RenovationRequest: ISerializable, INotifyPropertyChanged, IDataErrorInfo
    {
        public int Id { get; set; }
        private string comment;
        public string Comment 
        {
            get { return comment; } 
            set 
            { 
                if(comment != value)
                {
                    comment = value;
                    NotifyPropertyChanged(nameof(Comment));
                } 
            
            } 
        
        }
        private RENOVATIONLEVEL level;
        public RENOVATIONLEVEL Level 
        { 
            get {  return level; } 
            set 
            {
                if (level != value)
                {
                    level = value;
                    NotifyPropertyChanged(nameof(Level));
                }
            } 
        }

        private int reservationId;
        public int ReservationId { get {  return reservationId; } set {  reservationId = value; } }
        public Reservation Reservation;

        

        public RenovationRequest(string comment, RENOVATIONLEVEL level, Reservation reservation) 
        {
            this.Comment = comment;
            this.Level = level;
            Reservation = reservation;
        
        }
        public RenovationRequest(int reservationId)
        {
            ReservationId = reservationId;
        }

        public RenovationRequest() { }
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }


        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
               comment,
               Level.ToString(),
               reservationId.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            comment = values[1];
            reservationId = int.Parse(values[3]);
            if (values[2].Equals("Level1"))
            {
                Level = RENOVATIONLEVEL.Level1;
            }
            else if (values[2].Equals("Level2"))
            {
                Level = RENOVATIONLEVEL.Level2;
            }
            else if (values[2].Equals("Level3"))
            {
                Level = RENOVATIONLEVEL.Level3;
            }
            else if (values[2].Equals("Level4"))
            {
                Level = RENOVATIONLEVEL.Level4;
            }
            else
            {
                Level = RENOVATIONLEVEL.Level5;
            }
        }

        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                if (columnName == "Comment")
                {
                    if (string.IsNullOrEmpty(Comment))
                    {
                        return "Comment can not be empty";
                    }
                }
                else if (columnName == "Level")
                {
                    //if(Level.)
                }

                return null;
            }
        }

        private readonly string[] _validatedProperties = { "Comment", "Level" };
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
