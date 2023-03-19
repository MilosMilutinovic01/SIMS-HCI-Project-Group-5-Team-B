using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SIMS_HCI_Project_Group_5_Team_B.Serializer;

namespace SIMS_HCI_Project_Group_5_Team_B.Model
{
    public class OwnerGuestGrade : ISerializable, INotifyPropertyChanged
    {
        public int Id { get; set; }

        public int ReservationId { get; set; }

        public Reservation reservation { get; set; }

        public int cleanliness;
        public int Cleanliness
        {
            get { return cleanliness; }
            set
            {
                if(value != cleanliness)
                {
                    cleanliness = value;
                }
            }
        }

        public int rulesCompliance;
        public int RulesCompliance
        {
            get { return rulesCompliance; }
            set
            {
                if(value != rulesCompliance)
                {
                    rulesCompliance = value;
                }
            }
        }

        public int stateOfInventory;
        public int StateOfInventory
        {
            get { return stateOfInventory; }
            set 
            { 
                if(value != stateOfInventory) 
                { 
                    stateOfInventory = value; 
                } 
            }
        }

        public bool isPaymentCompletedOnTime;
        public bool IsPaymentCompletedOnTime
        {
            get { return isPaymentCompletedOnTime; }
            set
            {
                if(value != isPaymentCompletedOnTime)
                {
                    isPaymentCompletedOnTime = value;
                }
                    
            }
        }

        public bool complaintsFromOtherGuests;
        public bool ComplaintsFromOtherGuests
        {
            get { return complaintsFromOtherGuests; }

            set
            {
                if(value != complaintsFromOtherGuests)
                {
                    complaintsFromOtherGuests = value;
                }
            }
        }

        public string additionalComment;

        public string AdditionalComment
        {
            get { return additionalComment; }
            set
            {
                if(value != additionalComment)
                {
                    additionalComment = value;
                }
            }
        }

        public OwnerGuestGrade()
        {
            reservation = new Reservation();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
               Id.ToString(),
               ReservationId.ToString(),
               cleanliness.ToString(),
               rulesCompliance.ToString(),
               stateOfInventory.ToString(),
               isPaymentCompletedOnTime.ToString(),
               complaintsFromOtherGuests.ToString(),
               additionalComment
            };

            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            ReservationId = int.Parse(values[1]);
            cleanliness = int.Parse(values[2]);
            rulesCompliance = int.Parse(values[3]);
            stateOfInventory = int.Parse(values[4]);
            isPaymentCompletedOnTime = bool.Parse(values[5]);
            complaintsFromOtherGuests = bool.Parse(values[6]);
            additionalComment = values[7];
        }


    }
}
