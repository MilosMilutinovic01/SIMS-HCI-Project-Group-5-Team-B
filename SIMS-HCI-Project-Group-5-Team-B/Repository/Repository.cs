using SIMS_HCI_Project_Group_5_Team_B.Serializer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIMS_HCI_Project_Group_5_Team_B.Repository
{
    public class Repository<T> where T : class, ISerializable, new()
    {
        private List<T> _data = new List<T>();

        private string FilePath = "../../../Resources/Data/";

        private Serializer<T> _serializer;

        public Repository()
        {
            FilePath += typeof(T).Name + ".csv";

            _serializer = new Serializer<T>();
            _data = _serializer.FromCSV(FilePath);
        }

        public List<T> GetAll()
        {
            return _data;
        }

        public int NextId()
        {
            _data = _serializer.FromCSV(FilePath);
            if (_data.Count < 1)
            {
                return 1;
            }
            return _data.Max(d => d.Id) + 1;
        }

        public void Save(T obj)
        {
            obj.Id = NextId();
            _data = _serializer.FromCSV(FilePath);
            _data.Add(obj);
            _serializer.ToCSV(FilePath, _data);
        }

        public void SaveAll(List<T> obj)
        {
            foreach (T item in obj)
            {
                item.Id = NextId();
                _data = _serializer.FromCSV(FilePath);
                _data.Add(item);
                _serializer.ToCSV(FilePath, _data);
            }
        }
        public void Delete(T obj)
        {
            _data = _serializer.FromCSV(FilePath);
            T founded = _data.Find(d => d.Id == obj.Id);
            _data.Remove(founded);
            _serializer.ToCSV(FilePath, _data);
        }

        public void Update(T obj)
        {
            _data = _serializer.FromCSV(FilePath);
            T current = _data.Find(d => d.Id == obj.Id);
            int index = _data.IndexOf(current);
            _data.Remove(current);
            _data.Insert(index, obj);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _data);
        }

        /// <summary>
        /// Example of function call
        /// 
        /// class ModelOfSomething : ISerializable{
        ///     DateTime dt { get; set; }
        ///     SomeOtherClass soc {get; set; } //Some other class must have implemented ToString method
        ///     int number { get; set; }
        ///     ...
        /// }
        /// 
        /// DateTime date = new DateTime(2012, 12, 25, 10, 30, 50);
        /// SomeOtherClass something = new SomeOtherClass();
        /// 
        /// 
        /// Repository<ModelOfSomething> r = new Repository<ModelOfSomething>();
        /// r.FindBy( new string[] {dt, soc, number}, new string[] {date.ToString(), something.ToString(), "5"} );
        ///                   Names of Properties in class                Values converted to strings
        /// 
        /// </summary>
        /// <param name="propertyNames"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<T> FindBy(string[] propertyNames, string[] values)
        {
            if(propertyNames.Length != values.Length)
            {
                throw new Exception("Number of properties must be the same as the number of values");
            }
            if(propertyNames.Length == 0)
            {
                throw new Exception("Number of properties and values can't be 0");
            }

            Check(propertyNames);

            return Found(propertyNames, values);
        }

        public T FindId(int id)
        {
            return _data.Find(d => d.Id == id);
        }


        private void Check(string[] propertyNames)
        {
            foreach (string checkProperty in propertyNames)
            {
                bool propertyFound = false;
                foreach (var propery in typeof(T).GetProperties())
                {
                    if (checkProperty.Equals(propery.Name))
                    {
                        propertyFound = true;
                        break;
                    }
                }
                if (!propertyFound)
                {
                    throw new Exception("Given class " + nameof(T) + " doesn't contain property named " + checkProperty);
                }
            }
        }

        private List<T> Found(string[] propertyNames, string[] values)
        {
            List<T> result = new List<T>();

            foreach (var instance in _data)
            {
                bool equal = true;
                foreach (var originalProperty in instance.GetType().GetProperties())
                {
                    for (int i = 0; i < propertyNames.Length; i++)
                    {
                        if (propertyNames[i].Equals(originalProperty.Name))
                        {
                            if (!originalProperty.GetValue(instance).ToString().Equals(values[i]))
                            {
                                equal = false;
                                break;
                            }
                        }
                    }
                    if (!equal)
                    {
                        break;
                    }
                }
                if (equal)
                {
                    result.Add(instance);
                }
            }

            return result;
        }

    }
}
