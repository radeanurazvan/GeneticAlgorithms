using System;
using System.Collections.Generic;

namespace GeneticAlgorithmsHomeworks.Function
{
    public class DomainHelper
    {
        public static DimensionSet RandomNumbersInDomainRange(Domain domain, DimensionDefinition dimensionDefinition)
        {
            var numbers = new List<double>();
            for (var dimension = 1; dimension <= dimensionDefinition; dimension++)
            {
                var domainDefinition = domain.GetDefinitionForDimension(dimension);

                var rangeMultiplier = domainDefinition.End - domainDefinition.Start;
                var rangeDifference = domainDefinition.Start - 0;

                var random = new Random(DateTime.Now.Millisecond);

               numbers.Add(random.NextDouble() * rangeMultiplier + rangeDifference);
            }

            return new DimensionSet(numbers);
        }
    }
}