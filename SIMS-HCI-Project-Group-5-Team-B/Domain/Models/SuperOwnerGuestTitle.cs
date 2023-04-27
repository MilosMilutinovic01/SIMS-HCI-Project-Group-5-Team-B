using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.Models
{
    public class SuperOwnerGuestTitle: IDataErrorInfo
    {
        public int Id { get; set; }
        private DateTime activationDate;
        public DateTime ActivationDate 
        {
            get { return activationDate; }
            set { activationDate = value; }
        }

        private int availablePoints;
        public int AvailablePoints
        {
            get { return availablePoints; }
            set { availablePoints = value; }
        }

        private int ownerGuestId;
        public int OwnerGuestId 
        { 
            get { return ownerGuestId; }
            set
            {
                ownerGuestId = value;
            }
        }

        public OwnerGuest OwnerGuest { get; set; }

        public SuperOwnerGuestTitle() { }

        public SuperOwnerGuestTitle(DateTime activationDate, int availablePoints, int ownerGuestId)
        {
            ActivationDate = activationDate;
            AvailablePoints = availablePoints;
            OwnerGuestId = ownerGuestId;
        }

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "ActivationDate")
                {
                    if (ActivationDate < DateTime.Today)
                        return "Activation caan happen day by day";
                }
                else if (columnName == "AvailablePoints")
                {
                    if (AvailablePoints != 5)
                        return "5 points must be assigned";
                }
                
                return null;
            }
        }

        private readonly string[] _validatedProperties = { "ActivationDate", "AvailablePoints" };

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
