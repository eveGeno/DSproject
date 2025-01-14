using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSproject
{
    public class NonUniqueIndex<TKey, TRow> where TKey : notnull
    {
        private Dictionary<TKey, List<TRow>> _index = new Dictionary<TKey, List<TRow>>();

        public void Add(TKey key, TRow row)
        {
            if (!_index.ContainsKey(key))
            {
                _index[key] = new List<TRow>();
            }
            _index[key].Add(row);
        }

        public void Remove(TKey key)
        {
            _index.Remove(key);
        }


        public List<TRow> Find(TKey key)
        {
            return _index.ContainsKey(key) ? _index[key] : new List<TRow>();
        }
    }
}
