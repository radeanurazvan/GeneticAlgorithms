using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithmsHomeworks.Function
{
    public class Schwefel : DimensionalFunction
    {
        public override Domain GetDomain()
        {
            return new UniversalDomain(-500, 500);
        }

        public override DimensionDefinition GetDimensionDefinition()
        {
            return new DimensionDefinition(5);
        }

        protected override double GetValueCore(DimensionSet<double> tuple)
        {
            return tuple.Sum(x => SingleItemExpressionValue(x));
        }

        private static double SingleItemExpressionValue(double x)
        {
            var sinArg = Math.Sqrt(Math.Abs(x));
            var sinValue = Math.Sin(sinArg);

            return -x * sinValue;
        }
    }
}