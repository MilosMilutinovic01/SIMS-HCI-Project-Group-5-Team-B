using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.Models
{
    public class Comment:IDataErrorInfo
    {
        public int Id { get; set; } 
        //comment can be left by owner and owneGuest
        public int UserId { get; set; }
        public User User { get; set; }
        public int ForumId { get; set; }
        public string Content { get; set; }

        public string Error => null;

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

        public Comment() { }

        public Comment( int userId, int forumId, string content)
        {        
            UserId = userId;
            ForumId = forumId;
            Content = content;
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
