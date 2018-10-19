﻿using System;
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

        public override DimensionDefinition GetDimension()
        {
            return new DimensionDefinition(8);
        }

        public override double GetValue(IEnumerable<double> tuple)
        {
            return tuple.Sum(x => SingleItemExpressionValue(x));
        }

        private static double SingleItemExpressionValue(double x)
        {
            var sinArg = Math.Sqrt(x);
            var sinValue = Math.Sin(sinArg);

            return -x * sinValue;
        }
    }
}