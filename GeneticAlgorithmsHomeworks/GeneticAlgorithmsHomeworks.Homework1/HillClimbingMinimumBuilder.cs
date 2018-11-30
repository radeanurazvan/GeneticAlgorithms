using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticAlgorithmsHomeworks.Core;
using GeneticAlgorithmsHomeworks.Function;
using GeneticAlgorithmsHomeworks.Homework1.Improvement;

namespace GeneticAlgorithmsHomeworks.Homework1
{
    public class HillClimbingMinimumBuilder
    {
        private int iterations = 1;
        private int precision = 3;
        private DimensionalFunction function;
        private ImprovementStrategy strategy;

        public HillClimbingMinimumBuilder WithFunction(DimensionalFunction function)
        {
            this.function = function ?? throw new ArgumentException("The hill climbing minimum builder needs a non null function to work with!");

            return this;
        }

        public HillClimbingMinimumBuilder WithImprovementStrategy(ImprovementStrategy strategy)
        {
            this.strategy = strategy ?? throw new ArgumentException("The hill climbing minimum builder needs a non null strategy to work with!");

            return this;
        }

        public HillClimbingMinimumBuilder WithIterations(int iterations)
        {
            if (iterations <= 0)
            {
                throw new ArgumentException("The hill climbing minimum builder needs at least 1 iteration!");
            }
            this.iterations = iterations;

            return this;
        }

        public double Build()
        {
            function.Precision = precision;
            var minimum = double.MaxValue;

            for (var currentIteration = 1; currentIteration <= iterations; currentIteration++)
            {
                var randomState =
                    DomainHelper.RandomDimensionalBinaryValueInDomainRange(
                        function.GetDomain(), 
                        function.GetDimensionDefinition(), 
                        precision);

                var neighboursExhausted = false;

                while (!neighboursExhausted)
                {
                    var neighbourhood = GetNeighbourhood(randomState);
                    var improvement = strategy.PickImprovement(neighbourhood, function, minimum);

                    if (improvement == null)
                    {
                        neighboursExhausted = true;
                    }
                    else
                    {
                        minimum = function.GetValue(improvement, new BinarySetToDoubleSetConverter());
                    }
                }
            }

            return minimum;
        }

        private static IEnumerable<DimensionSet<BinaryRepresentation>> GetNeighbourhood(DimensionSet<BinaryRepresentation> subject)
        {
            return subject.SelectMany((bitRepresentation, index) =>
            {
                var allAlterations = GetAlteredRepresentations(bitRepresentation);

                var neighbourhood = new List<DimensionSet<BinaryRepresentation>>();
                foreach (var alteration in allAlterations)
                {
                    var neighbour = subject.ToList();
                    neighbour[index] = alteration;

                    neighbourhood.Add(new DimensionSet<BinaryRepresentation>(neighbour));
                }
                return neighbourhood;

            }).ToList();
        }

        private static IEnumerable<BinaryRepresentation> GetAlteredRepresentations(BinaryRepresentation bitRepresentation)
        {
            return bitRepresentation.Value.Select((bit, index)=>
            {
                var alteredBit = bit == '0' ? '1' : '0';
                var alteredRepresentation = new StringBuilder(bitRepresentation.Value);
                alteredRepresentation[index] = alteredBit;

                return BinaryRepresentation.Create(alteredRepresentation.ToString());
            });
        }
    }
}