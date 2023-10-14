using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Things_DB
{
    internal class Things
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Supplyer { get; set; }
        public int Count { get; set; }
        public int Cost { get; set; }
        public DateTime Date { get; set; }
        public override string ToString()
        {
            return $"{Id} - {Name} - {Type} - {Supplyer} - {Count} - {Cost} - {Date}";
        }

    }
}
