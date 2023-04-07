using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_HCI_Project_Group_5_Team_B.Serializer;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.Models
{
    public class Owner : ISerializable
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

        /*private int numberOfReservations;
        public int NumberReservations
        {
            get { return numberOfReservations; }
            set
            {
                if(value != numberOfReservations)
                {
                    numberOfReservations = value;
                }
            }
        }*/

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
            name = values[1];
            surname = values[2];
            gradeAverage = double.Parse(values[3]);
            //numberOfReservations =int.Parse(values[4]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                name,
                surname,
                gradeAverage.ToString(),
                //numberOfReservations.ToString()
            };
            return csvValues;
        }

    }
}
