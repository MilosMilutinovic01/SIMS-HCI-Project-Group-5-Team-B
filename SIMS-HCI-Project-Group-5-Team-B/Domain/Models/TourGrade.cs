using SIMS_HCI_Project_Group_5_Team_B.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.Models
{
    public class TourGrade : ISerializable, IDataErrorInfo, INotifyPropertyChanged
    {
        public int Id { get; set; }

        private int guideGeneralKnowlegde;
        public int GuideGeneralKnowlegde
        {
            get { return guideGeneralKnowlegde; }
            set
            {
                if(value != guideGeneralKnowlegde)
                {
                    guideGeneralKnowlegde = value;
                    OnPropertyChanged();
                }
            }
        }

        private int guideLanguageKnowledge;
        public int GuideLanguageKnowledge
        {
            get { return guideLanguageKnowledge; }
            set
            {
                if(value != guideLanguageKnowledge)
                {
                    guideLanguageKnowledge = value;
                    OnPropertyChanged();
                }
            }
        }

        private int tourFun;
        public int TourFun
        {
            get { return tourFun; }
            set
            {
                if(value != tourFun)
                {
                    tourFun = value;
                    OnPropertyChanged();
                }
            }
        }

        private string imageUrls;
        public string ImageUrls
        {
            get { return imageUrls; }
            set
            {
                if(value != imageUrls)
                {
                    imageUrls = value;
                    OnPropertyChanged();
                }
            }
        }

        private int tourAttendanceId;
        public int TourAttendanceId
        {
            get { return tourAttendanceId; }
            set
            {
                if(value != tourAttendanceId)
                {
                    tourAttendanceId = value;
                    OnPropertyChanged();
                }
            }
        }

        private int guideGuestId;
        public int GuideGuestId
        {
            get { return guideGuestId; }
            set
            {
                if(value != guideGuestId)
                {
                    guideGuestId = value;
                    OnPropertyChanged();
                }
            }
        }

        public TourGrade() { }

        public TourGrade(int id, int guideGeneralKnowlegde, int guideLanguageKnowledge, int tourFun, string imageUrls, int tourAttendanceId, int guideGuestId)
        {
            Id = id;
            GuideGeneralKnowlegde = guideGeneralKnowlegde;
            GuideLanguageKnowledge = guideLanguageKnowledge;
            TourFun = tourFun;
            ImageUrls = imageUrls;
            TourAttendanceId = tourAttendanceId;
            GuideGuestId = guideGuestId;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                guideGeneralKnowlegde.ToString(),
                guideLanguageKnowledge.ToString(),
                tourFun.ToString(),
                imageUrls
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            guideGeneralKnowlegde = int.Parse(values[1]);
            guideLanguageKnowledge = int.Parse(values[2]);
            tourFun = int.Parse(values[3]);
            imageUrls = values[4];
        }


        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "GuideGeneralKnowlegde")
                {
                    if (GuideGeneralKnowlegde > 5 || GuideGeneralKnowlegde < 1)
                        return "Grade must be between 1 and 5";
                }
                else if (columnName == "GuideLanguageKnowledge")
                {
                    if (GuideLanguageKnowledge > 5 || GuideLanguageKnowledge < 1)
                        return "Grade must be between 1 and 5";
                }
                else if (columnName == "TourFun")
                {
                    if (TourFun > 5 || TourFun < 1)
                        return "Grade must be between 1 and 5";
                }
                return null;
            }
        }

        private readonly string[] _validatedProperties = { "GuideGeneralKnowlegde", "GuideLanguageKnowledge", "TourFun" };

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
    }
}
