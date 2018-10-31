﻿using System;
using System.Linq;

namespace GeneticAlgorithmsHomeworks.Function
{
    public class DeJong : DimensionalFunction
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
            return tuple.Sum(x => x * x);
        }
    }
}