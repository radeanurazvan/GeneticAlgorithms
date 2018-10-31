using System.Collections.Generic;

namespace GeneticAlgorithmsHomeworks.Function
{
    public abstract class DimensionalFunction
    {
        public abstract Domain GetDomain();

        public abstract DimensionDefinition GetDimensionDefinition();

        public abstract double GetValue(DimensionSet values);
    }
}