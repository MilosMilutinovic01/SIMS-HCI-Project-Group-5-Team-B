using SIMS_HCI_Project_Group_5_Team_B.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Model
{
    public class KeyPoints : ISerializable, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                }
            }
        }

        public int order;
        public int Order
        {
            get { return order; }
            set
            {
                if (order != value)
                {
                    order = value;
                }
            }
        }

        public bool selected;
        public bool Selected
        {
            get { return selected; }
            set
            {
                if (selected != value)
                {
                    selected = value;
                }
            }
        }

        private int tourId;
        public int TourId
        {
            get { return tourId; }
            set
            {
                if (value != tourId)
                {
                    tourId = value;
                    OnPropertyChanged();
                }
            }
        }
        public KeyPoints() { }
        public KeyPoints(string name, bool selected, int order, int tourId)
        {
            this.name = name;
            this.order = order;
            this.selected = selected;
            this.tourId = tourId;
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                name,
                order.ToString(),
                selected.ToString(),
                tourId.ToString()
            };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            name = values[1];
            order = int.Parse(values[2]);
            selected = Convert.ToBoolean(values[3]);
            tourId = int.Parse(values[4]);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
