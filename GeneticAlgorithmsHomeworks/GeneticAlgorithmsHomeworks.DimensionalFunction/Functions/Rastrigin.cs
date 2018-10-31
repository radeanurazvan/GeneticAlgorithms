using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithmsHomeworks.Function
{
    public class Rastrigin : DimensionalFunction
    {
        public override Domain GetDomain()
        {
            return new UniversalDomain(-5.12, 5.12);
        }

        public override DimensionDefinition GetDimensionDefinition()
        {
            return new DimensionDefinition(2);
        }

        protected override double GetValueCore(DimensionSet tuple)
        {
            return 10 * GetDimensionDefinition() + tuple.Sum(x => SingleItemSumValue(x));
        }

        private static double SingleItemSumValue(double x)
        {
            var cosArg = 2 * Math.PI * x;
            var cosValue = Math.Cos(cosArg);
            var square = x * x;

            return square - 10.0 * cosValue;
        }
    }
}