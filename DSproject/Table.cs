using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSproject
{
    public class Table<T>
    {
        private LinkedList<T> _rows = new LinkedList<T>();

        public void AddRow(T row)
        {
            _rows.AddLast(row);
        }

        public void RemoveRow(T row)
        {
            _rows.Remove(row);
        }

        public IEnumerable<T> GetAllRows()
        {
            return _rows;
        }
    }
}
