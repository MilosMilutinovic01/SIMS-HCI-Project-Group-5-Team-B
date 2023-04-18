using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIMS_HCI_Project_Group_5_Team_B.Repository
{
    public class KeyPointCSVRepository : CSVRepository<KeyPoint>, IKeyPointRepository
    {
        public KeyPointCSVRepository() :base() { }
        public void Delete(KeyPoint keyPoint)
        {
            KeyPoint? founded = _data.Find(d => d.Id == keyPoint.Id);
            if (founded != null)
            {
                _data.Remove(founded);
                WriteCSV(_data);
            }
        }

        public List<KeyPoint> GetAll()
        {
            return _data;
        }

        public void Save(KeyPoint newKeyPoint)
        {
            newKeyPoint.Id = NextId();
            _data.Add(newKeyPoint);
            WriteCSV(_data);
        }
        private int NextId()
        {
            if (_data.Count() < 1)
            {
                return 1;
            }
            else
            {
                return _data.Max(d => d.Id) + 1;
            }
        }

        public void Update(KeyPoint keyPoint)
        {
            KeyPoint? current = _data.Find(d => d.Id == keyPoint.Id);
            if (current == null)
            {
                Save(keyPoint);
                return;
            }
            int index = _data.IndexOf(current);
            _data.Remove(current);
            _data.Insert(index, keyPoint);
            WriteCSV(_data);
        }

        protected override KeyPoint FromCSV(string[] values)
        {
            KeyPoint newKeyPoint = new KeyPoint();

            newKeyPoint.Id = int.Parse(values[0]);
            newKeyPoint.Name = values[1];
            newKeyPoint.Selected = Convert.ToBoolean(values[2]);
            newKeyPoint.TourId = int.Parse(values[3]);

            return newKeyPoint;
        }

        protected override string[] ToCSV(KeyPoint obj)
        {
            string[] csvValues =
{
                obj.Id.ToString(),
                obj.Name,
                obj.Selected.ToString(),
                obj.TourId.ToString()
            };

            return csvValues;
        }
    }
}
