using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_HCI_Project_Group_5_Team_B.Serializer;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.Models
{
    public class Owner : User, ISerializable
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

        private double gradeAverage;

        public double GradeAverage
        {
            get { return gradeAverage; }
            set
            {
                if (value != gradeAverage)
                {
                    gradeAverage = value;
                }
            }
        }

        private bool isSuperOwner;

        public bool IsSuperOwner
        {
            get { return isSuperOwner; }
            set
            {
                if(value != isSuperOwner)
                {
                    isSuperOwner = value;
                }
            }
        }

        public Owner()
        {
            //initially, there is only one owner, in order to not complicate the implementation of other features

            Id = 0;
            Name = "Nina";
            Surname = "Kuzminac";
        }
        public Owner(string name, string surname)
        {
            //initially, there is only one owner, in order to not complicate the implementation of other features
            Name = name;
            Surname = surname;
            gradeAverage = 0;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Username = values[1];
            Password = values[2];
            name = values[3];
            surname = values[4];
            gradeAverage = double.Parse(values[5]);
            isSuperOwner = bool.Parse(values[6]);
            //numberOfReservations =int.Parse(values[4]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Username,
                Password,
                name,
                surname,
                gradeAverage.ToString(),
                isSuperOwner.ToString()
                //numberOfReservations.ToString()
            };
            return csvValues;
        }

    }
}
