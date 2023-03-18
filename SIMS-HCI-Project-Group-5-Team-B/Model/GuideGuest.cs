﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Model
{
    public class GuideGuest
    {
        public int Id { get; set; }
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string surname;
        public string Surname
        {
            get { return surname; }
            set { name = value; }
        }
        private bool confirmation;
        public bool Confirmation
        {
            get { return confirmation; }
            set { confirmation = value; }
        }
        private bool answer;
        public bool Answer
        {
            get { return answer; }
            set { answer = value; }
        }
        public GuideGuest()
        {
            //initially, there is only one guest, in order to not complicate the implementation of other features
        }

        public GuideGuest(int id, string name, string surname)
        {
            this.Id = id;
            this.name = name;
            this.surname = surname;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            name = values[1];
            surname = values[2];
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                name,
                surname
            };
            return csvValues;
        }
    }
}
