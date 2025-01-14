using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSproject
{
    public class UniqueCompositeKeyIndex<TKey1, TKey2, TRow>
    {
        private Dictionary<Tuple<TKey1, TKey2>, TRow> _index = new Dictionary<Tuple<TKey1, TKey2>, TRow>();

        public void Add(TKey1 key1, TKey2 key2, TRow row)
        {
            var compositeKey = Tuple.Create(key1, key2);
            if (_index.ContainsKey(compositeKey))
                throw new InvalidOperationException("Duplicate composite key.");
            _index[compositeKey] = row;
        }

        public void Remove(TKey1 key1, TKey2 key2)
        {
            var compositeKey = Tuple.Create(key1, key2);
            _index.Remove(compositeKey);
        }

        public TRow Find(TKey1 key1, TKey2 key2)
        {
            var compositeKey = Tuple.Create(key1, key2);
            return _index.ContainsKey(compositeKey) ? _index[compositeKey] : default(TRow);
        }
    }
}
