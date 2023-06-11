using SIMS_HCI_Project_Group_5_Team_B.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.Models
{
    public class Guide: User, ISerializable
    {
        public int Id { get; set; }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (value != name)
                {
                    name = value;
                }
            }
        }
        private string surname;
        public string Surname
        {
            get { return surname; }
            set
            {
                if (value != surname)
                {
                    surname = value;
                }
            }
        }

        private bool isSuperGuide;
        public bool IsSuperGuide
        {
            get { return isSuperGuide; }
            set 
            {
                if (value != isSuperGuide)
                {
                    isSuperGuide = value;
                }
            }
        }

        private double averageGrade;
        public double AverageGrade
        {
            get { return averageGrade; }
            set
            {
                if (value != averageGrade)
                {
                    averageGrade = value;
                }
            }
        }
        private bool resigned;
        public bool Resigned
        {
            get { return resigned; }
            set
            {
                if (value != resigned)
                {
                    resigned = value;
                }
            }
        }
        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                if (value != username)
                {
                    username = value;
                }
            }
        }

        public Guide()
        {
        
        }
        public Guide(string username, string name, string surname)
        {
            Username = username;
            Name = name;
            Surname = surname;
            IsSuperGuide = false;
            AverageGrade = 0;
            Resigned = false;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            Surname = values[2];
            IsSuperGuide = Convert.ToBoolean(values[3]);
            AverageGrade = Convert.ToDouble(values[4]);
            Resigned = Convert.ToBoolean(values[5]);
            Username = values[6];
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Name,
                Surname,
                IsSuperGuide.ToString(),
                AverageGrade.ToString(),
                Resigned.ToString(),
                Username.ToString()
            };
            return csvValues;
        }
    }
}
