using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithmsHomeworks.Function
{
    public class DeJong : DimensionalFunction
    {
        public override Domain GetDomain()
        {
            return new UniversalDomain(-5.12, 5.12);
        }

        public override DimensionDefinition GetDimension()
        {
            return new DimensionDefinition(5);
        }

        public override double GetValue(IEnumerable<double> tuple)
        {
            return tuple.Sum(x => x * x);
        }
    }
}