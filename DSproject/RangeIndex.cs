using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSproject
{
    public class RangeIndex<TKey, TRow> where TKey : IComparable<TKey>
    {
        private SortedDictionary<TKey, TRow> _index = new SortedDictionary<TKey, TRow>();

        public void Add(TKey key, TRow row)
        {
            _index[key] = row;
        }

        public void Remove(TKey key)
        {
            _index.Remove(key);
        }

        public IEnumerable<TRow> FindRange(TKey start, TKey end)
        {
            return _index.Where(kvp => kvp.Key.CompareTo(start) >= 0 && kvp.Key.CompareTo(end) <= 0).Select(kvp => kvp.Value);
        }
    }
}
