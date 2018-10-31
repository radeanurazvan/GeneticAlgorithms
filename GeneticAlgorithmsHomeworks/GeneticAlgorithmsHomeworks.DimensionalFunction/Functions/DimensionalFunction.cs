using System;
using System.Collections.Generic;

namespace GeneticAlgorithmsHomeworks.Function
{
    public abstract class DimensionalFunction
    {
        public abstract Domain GetDomain();

        public abstract DimensionDefinition GetDimensionDefinition();

        protected abstract double GetValueCore(DimensionSet tuple);

        public double GetValue(DimensionSet tuple)
        {
            return Math.Round(GetValueCore(tuple), 5);
        }
    }
}