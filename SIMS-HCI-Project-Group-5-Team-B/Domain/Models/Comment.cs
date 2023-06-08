using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.Models
{
    public class Comment:IDataErrorInfo, INotifyPropertyChanged
    {
        public int Id { get; set; } 
        //comment can be left by owner and owneGuest
        public int UserId { get; set; }
        public User User { get; set; }
        public int ForumId { get; set; }
        public string Content { get; set; }
        public bool IsFromOwnerWithAccommodationOnLocation { get; set; }
        public bool WasNotOnLocation { get; set; }
        public bool CanReport { get; set; }
        private int numberOfReports;
        public int NumberOfReports{
            get { return numberOfReports; }
            set
            {
                numberOfReports = value;
                NotifyPropertyChanged(nameof(NumberOfReports));
            }
        }
        public List<int> ownersWhoReportedComment;

        private string ownersWhoReportedCommentString;

        public string OwnersWhoReportedCommentString
        {
            get { return ownersWhoReportedCommentString; }
            set
            {
                if (value != ownersWhoReportedCommentString)
                {
                    ownersWhoReportedCommentString = value;
                    NotifyPropertyChanged(nameof(OwnersWhoReportedCommentString));
                }
            }
        }

        public string Error => null;

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        public string this[string columnName]
        {
            get
            {
                if(columnName == "Content")
                {
                    if (string.IsNullOrEmpty(Content))
                    {
                        return "Comment can not be empty";
                    }
                    
                }
                return null;
            }
        }

        public Comment() {
            ownersWhoReportedComment = new List<int>();
        }

        public Comment( int userId, int forumId, string content)
        {        
            UserId = userId;
            ForumId = forumId;
            Content = content;
            ownersWhoReportedComment = new List<int>();
        }
        private readonly string[] _validatedProperties = { "Content" };
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
