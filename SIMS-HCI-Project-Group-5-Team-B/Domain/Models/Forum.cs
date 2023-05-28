﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.Models
{
    public enum FORUMSTATUS {Active = 0, Closed};
    public class Forum
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public int OwnerGuestId { get; set; }
        public OwnerGuest ForumOwner { get; set; }
        public FORUMSTATUS ForumStatus { get; set; }

        public List<Comment> Comments { get; set; }

        public bool IsVeryUseful { get; set; }

        public Forum()
        {
            Comments = new List<Comment>();        
        }

        public Forum(int locationId, int ownerGuestId, bool veryUseful)
        {
            this.LocationId = locationId;
            this.OwnerGuestId = ownerGuestId;
            Comments = new List<Comment>();
            ForumStatus = FORUMSTATUS.Active;
            this.IsVeryUseful = veryUseful;
        }
    }
}