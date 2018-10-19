using System.Collections.Generic;

namespace GeneticAlgorithmsHomeworks.Function
{
    public abstract class DimensionalFunction
    {
        public abstract Domain GetDomain();

        public abstract DimensionDefinition GetDimension();

        public abstract double GetValue(IEnumerable<double> tuple);
    }
}