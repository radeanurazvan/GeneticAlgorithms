using System;
using System.Collections.Generic;

namespace GeneticAlgorithmsHomeworks.Function
{
    public class DomainHelper
    {
        public static IEnumerable<double> RandomNumbersInDomainRange(Domain domain, DimensionDefinition dimensionDefinition)
        {
            for (var dimension = 1; dimension <= dimensionDefinition; dimension++)
            {
                var domainDefinition = domain.GetDefinitionForDimension(dimension);

                var rangeMultiplier = domainDefinition.End - domainDefinition.Start;
                var rangeDifference = domainDefinition.Start - 0;

                var random = new Random(DateTime.Now.Millisecond);

                yield return (random.NextDouble() * rangeMultiplier) + rangeDifference;
            }
        }
    }
}