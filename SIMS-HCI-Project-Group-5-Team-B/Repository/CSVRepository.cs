using SIMS_HCI_Project_Group_5_Team_B.Serializer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Repository
{
    public abstract class CSVRepository<T> where T : class
    {
        private const char Delimiter = '|';

        private string FilePath = "../../../Resources/Data/";

        protected List<T> _data;

        public CSVRepository()
        {
            FilePath += typeof(T).Name + ".csv";
            _data = ReadCSV();
        }

        protected void WriteCSV(List<T> objects)
        {
            StringBuilder csv = new StringBuilder();

            foreach (T obj in objects)
            {
                string line = string.Join(Delimiter.ToString(), ToCSV(obj));
                csv.AppendLine(line);
            }

            File.WriteAllText(FilePath, csv.ToString());
        }

        protected List<T> ReadCSV()
        {
            List<T> objects = new List<T>();

            foreach (string line in File.ReadLines(FilePath))
            {
                string[] csvValues = line.Split(Delimiter);
                T obj = FromCSV(csvValues);
                objects.Add(obj);
            }

            return objects;
        }
        protected abstract string[] ToCSV(T obj);
        protected abstract T FromCSV(string[] values);
    }
}
