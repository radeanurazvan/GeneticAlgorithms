using System;
using System.Linq;
using GeneticAlgorithmsHomeworks.Core;

namespace GeneticAlgorithmsHomeworks.Function
{
    public abstract class DimensionalFunction
    {
        protected DimensionalFunction()
        {
            Dimension = GetDimensionDefinition();
        }

        protected DimensionDefinition Dimension { get; set; }

        protected bool DimensionLocked { get; set; }

        public int Precision { get; set; }

        public abstract Domain GetDomain();

        public abstract DimensionDefinition GetDimensionDefinition();

        protected abstract double GetValueCore(DimensionSet<double> tuple);

        public bool TrySetDimension(int dimension)
        {
            if (DimensionLocked)
            {
                return false;
            }

            Dimension = new DimensionDefinition(dimension);
            return true;
        }

        public double GetValue(DimensionSet<double> tuple)
        {
            return Math.Round(GetValueCore(tuple), Precision);
        }

        public double GetValue<TSetType, TToDoubleSetConverter>(
            DimensionSet<TSetType> tuple,
            TToDoubleSetConverter converter)

            where TToDoubleSetConverter : FunctionSetToDoubleSetConverter<TSetType>
        {
            var doubleSet = converter.Convert(tuple, this);

            return GetValue(new DimensionSet<double>(doubleSet));
        }
    }
}