using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSproject
{
    public class DatabaseEngine<T>
    {
        private Table<T> _table = new Table<T>();
        private List<object> _indices = new List<object>(); // Holds all index types

        public void AddIndex(object index)
        {
            _indices.Add(index);
        }

        public void AddRecord(T row)
        {
            //Check duplicate by ID
            int id = (int)typeof(T).GetProperty("Id").GetValue(row);

            if (IdExists(id))
            {
                //Generate unique ID
                int newId = GenerateUniqueId();
                typeof(T).GetProperty("Id").SetValue(row, newId);
            }

            _table.AddRow(row);

            foreach (var index in _indices)
            {
                var addMethod = index.GetType().GetMethod("Add");

                // Get key for index
                var key = GetKeyForIndex(index, row);

                // Checking the key type and parameter of the Add method
                var parameterType = addMethod.GetParameters()[0].ParameterType;
                if (parameterType.IsInstanceOfType(key))
                {
                    addMethod.Invoke(index, new object[] { key, row });
                }
                else
                {
                    throw new ArgumentException($"The key type {key.GetType()} does not match the expected type {parameterType} for the index.");
                }
            }
        }

        private bool IdExists(int id)
        {
            //Check if ID exists in table
            return _table.GetAllRows().Any(row => (int)typeof(T).GetProperty("Id").GetValue(row) == id);
        }

        private int GenerateUniqueId()
        {
            //Generate new ID
            Random random = new Random();
            int newId;

            do
            {
                newId = random.Next(100, 1000); 
            } while (IdExists(newId));

            return newId;
        }

        //Method to get all customers from a specific company
        public IEnumerable<T> GetRecordsByCompany(string companyName)
        {
            return _table.GetAllRows().Where(row => (row as Customer)?.CompanyName == companyName);
        }

        //Method to get all customers from a specific country
        public IEnumerable<T> GetRecordsByCountry(string country)
        {
            return _table.GetAllRows().Where(row => (row as Customer)?.Country == country);
        }

        public void RemoveRecord(T row)
        {
            // Delete record from table
            _table.RemoveRow(row);

            foreach (var index in _indices)
            {
                // get the Remove method dynamically
                var removeMethod = index.GetType().GetMethod("Remove");
                if (removeMethod == null)
                {
                    throw new InvalidOperationException("Remove method not found on index.");
                }

                // Get the key for current index
                var key = GetKeyForIndex(index, row);

                // Check null
                if (key == null)
                {
                    throw new InvalidOperationException("Key cannot be null when removing from index.");
                }

                // Call remove method
                removeMethod.Invoke(index, new object[] { key });
            }
        }

        public void UpdateRecord(T oldRow, T newRow)
        {
            // Delete old row
            RemoveRecord(oldRow);

            // Add new record
            AddRecord(newRow);
        }

        public void CreateUniqueIndex<TKey>(Func<T, TKey> keySelector) where TKey : notnull
        {
            var newIndex = new UniqueIndex<TKey, T>();

            // Filling the index with existing data
            foreach (var row in _table.GetAllRows())
            {
                var key = keySelector(row);
                newIndex.Add(key, row);
            }

            AddIndex(newIndex);
        }

        public void CreateIndex<TKey>(string propertyName, bool isUnique = true)
        where TKey : notnull
        {
            if (isUnique)
            {
                var uniqueIndex = new UniqueIndex<TKey, T>();
                foreach (var row in _table.GetAllRows())
                {
                    var keyObject = typeof(T).GetProperty(propertyName)?.GetValue(row);
                    if (keyObject != null)
                    {
                        var key = (TKey)keyObject;
                        uniqueIndex.Add(key, row);
                    }
                    else
                    {
                        throw new ArgumentNullException(nameof(keyObject), $"The key value for '{propertyName}' cannot be null.");
                    }
                }
                _indices.Add(uniqueIndex);
            }
            else
            {
                var nonUniqueIndex = new NonUniqueIndex<TKey, T>();
                foreach (var row in _table.GetAllRows())
                {
                    var keyObject = typeof(T).GetProperty(propertyName)?.GetValue(row);
                    if (keyObject != null)
                    {
                        var key = (TKey)keyObject;
                        nonUniqueIndex.Add(key, row);
                    }
                    else
                    {
                        throw new ArgumentNullException(nameof(keyObject), $"The key value for '{propertyName}' cannot be null.");
                    }
                }
                _indices.Add(nonUniqueIndex);
            }
        }

        private object GetKeyForIndex(object index, T row)
        {
            // Checking index type and extracting from the row object
            if (index is UniqueIndex<int, T>)
            {
                return typeof(T).GetProperty("Id")?.GetValue(row);
            }
            else if (index is NonUniqueIndex<string, T>)
            {
                return typeof(T).GetProperty("CompanyName")?.GetValue(row);
            }
            else if (index is RangeIndex<int, T>)
            {
                return typeof(T).GetProperty("Id")?.GetValue(row);
            }
            else
            {
                throw new InvalidOperationException("Unknown index type.");
            }
        }

        public void RemoveIndex(object index)
        {
            _indices.Remove(index);
        }

        public IEnumerable<T> GetAllRecords()
        {
            return _table.GetAllRows();
        }

        public T FindRecordByUniqueIndex<TKey>(UniqueIndex<TKey, T> uniqueIndex, TKey key) where TKey : notnull
        {
            return uniqueIndex.Find(key);
        }

        public List<T> FindRecordsByNonUniqueIndex<TKey>(NonUniqueIndex<TKey, T> nonUniqueIndex, TKey key)
        where TKey : notnull
        {
            return nonUniqueIndex.Find(key);
        }

        public IEnumerable<T> FindRecordsByRange<TKey>(RangeIndex<TKey, T> rangeIndex, TKey start, TKey end)
        where TKey : IComparable<TKey>
        {
            return rangeIndex.FindRange(start, end);
        }

        public IEnumerable<T> FindRecords(Func<T, bool> predicate)
        {
            // Trying to use index for search
            foreach (var index in _indices)
            {
                if (index is UniqueIndex<int, T> uniqueIndex)
                {
                    var key = GetKeyFromPredicate(predicate, "Id");
                    if (key != null && uniqueIndex.Find((int)key) is T result) return new List<T> { result };
                }
                else if (index is NonUniqueIndex<string, T> nonUniqueIndex)
                {
                    var key = GetKeyFromPredicate(predicate, "CompanyName");
                    if (key != null) return nonUniqueIndex.Find((string)key);
                }
            }

            // Linear search if index not found
            return _table.GetAllRows().Where(predicate);
        }

        // Helper method to extract key from predicate
        private object GetKeyFromPredicate(Func<T, bool> predicate, string propertyName)
        {
            var property = typeof(T).GetProperty(propertyName);
            foreach (var row in _table.GetAllRows())
            {
                if (predicate(row)) return property?.GetValue(row);
            }
            return null;
        }
    }
}
