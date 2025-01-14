using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSproject
{
    public class UniqueIndex<TKey, TRow> where TKey : notnull
    {
        private Dictionary<TKey, TRow> _index = new Dictionary<TKey, TRow>();

        public bool Add(TKey key, TRow row)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key), "Key cannot be null.");

            if (_index.ContainsKey(key))
                return false; //The key already exists, adding failed

            _index[key] = row;
            return true; //added succesfully
        }

        public void Remove(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key), "Key cannot be null.");

            _index.Remove(key);
        }

        public TRow? Find(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key), "Key cannot be null.");

            //Trying to find a string by key
            if (_index.TryGetValue(key, out var row))
            {
                return row; //Return the found string
            }

            return default; //Return default value for TRow if not found
        }
    }

}
