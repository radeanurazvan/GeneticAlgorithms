using System;
using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithmsHomeworks.Core;

namespace GeneticAlgorithmsHomeworks.Function
{
    public abstract class DimensionalFunction
    {
        public int Precision { get; set; }

        public abstract Domain GetDomain();

        public abstract DimensionDefinition GetDimensionDefinition();

        protected abstract double GetValueCore(DimensionSet<double> tuple);

        public double GetValue(DimensionSet<double> tuple)
        {
            return Math.Round(GetValueCore(tuple), Precision);
        }

        public double GetValue(DimensionSet<BinaryRepresentation> tuple)
        {
            var doubleSet = tuple.Select((x, dimension) => BinaryHelper.DecodeBinary(x, GetDomain().GetDefinitionForDimension(dimension+1), Precision));

            return GetValue(new DimensionSet<double>(doubleSet));
        }
    }
}