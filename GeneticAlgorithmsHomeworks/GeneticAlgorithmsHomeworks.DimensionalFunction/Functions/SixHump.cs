﻿using System;
using System.Linq;

namespace GeneticAlgorithmsHomeworks.Function
{
    public class SixHump : DimensionalFunction
    {
        public SixHump()
        {
            DimensionLocked = true;
        }

        public override Domain GetDomain()
        {
            return DimensionalDomain.FromDimension(GetDimensionDefinition())
                .WithDefinition(new DomainDefinition(-3, 3))
                .WithDefinition(new DomainDefinition(-2, 2));
        }

        public override DimensionDefinition GetDimensionDefinition()
        {
            return new DimensionDefinition(2);
        }

        protected override double GetValueCore(DimensionSet<double> tuple)
        {
            var firstParam = tuple.ElementAt(0);
            var secondParam = tuple.ElementAt(1);

            var firstSquare = firstParam * firstParam;
            var secondSquare = secondParam * secondParam;

            var value = (4 - 2.1 * firstSquare + Math.Pow(firstParam, 4) / 3) * firstSquare;
            value += firstParam * secondParam;
            value += (-4 + 4 * secondSquare) * secondSquare;

            return value;
        }
    }
}