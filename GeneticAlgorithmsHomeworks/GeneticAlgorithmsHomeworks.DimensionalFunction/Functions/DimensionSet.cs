using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithmsHomeworks.Function
{
    public class DimensionSet<T> : IEnumerable<T>
    {
        private readonly ICollection<T> values = new List<T>();

        public DimensionSet(IEnumerable<T> set)
        {
            values = set.ToList();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}