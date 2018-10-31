using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithmsHomeworks.Function
{
    public class DimensionSet : IEnumerable<double>
    {
        private readonly ICollection<double> values = new List<double>();

        public DimensionSet(IEnumerable<double> set)
        {
            values = set.ToList();
        }

        public IEnumerator<double> GetEnumerator()
        {
            return values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}