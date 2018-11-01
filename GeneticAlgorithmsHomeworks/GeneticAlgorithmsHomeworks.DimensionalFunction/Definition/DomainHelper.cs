using System;
using System.Collections.Generic;
using System.Text;
using GeneticAlgorithmsHomeworks.Core;

using static System.Math;

namespace GeneticAlgorithmsHomeworks.Function
{
    public class DomainHelper
    {
        public static DimensionSet<double> RandomNumbersInDomainRange(Domain domain, DimensionDefinition dimensionDefinition)
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

            return new DimensionSet<double>(numbers);
        }

        public static DimensionSet<BinaryRepresentation> RandomBinaryNumbersInDomainRange(
            Domain domain, 
            DimensionDefinition dimensionDefinition, 
            int precision)
        {
            var numbers = new List<BinaryRepresentation>();
            for (var dimension = 1; dimension <= dimensionDefinition; dimension++)
            {
                var bitsNumber = BinaryHelper.BitsNumberForDomainDimension(domain.GetDefinitionForDimension(dimension), precision);
                var representation = new StringBuilder();

                for (var i = 1; i <= bitsNumber; i++)
                {
                    var random = new Random(Guid.NewGuid().GetHashCode()).NextDouble();
                    var bit = Round(random).ToString();
                    representation.Append(bit);
                }

                numbers.Add(BinaryRepresentation.Create(representation.ToString()));
            }

            return new DimensionSet<BinaryRepresentation>(numbers);
        }
    }
}