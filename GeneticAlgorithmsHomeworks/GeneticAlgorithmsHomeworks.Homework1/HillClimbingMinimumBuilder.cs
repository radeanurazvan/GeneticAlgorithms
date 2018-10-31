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
        private DimensionalFunction function;
        private ImprovementStrategy strategy;

        public HillClimbingMinimumBuilder WithIterations(int iterations)
        {
            if (iterations <= 0)
            {
                throw new ArgumentException("The hill climbing minimum builder should iterate at least once!");
            }

            this.iterations = iterations;
            return this;
        }

        public HillClimbingMinimumBuilder WithFunction(DimensionalFunction function)
        {
            this.function = function ?? throw new ArgumentNullException("The hill climb minimum builder needs a non-null function to work with!");
            return this;
        }

        public HillClimbingMinimumBuilder WithImprovementStrategy(ImprovementStrategy strategy)
        {
            this.strategy = strategy ?? throw new ArgumentNullException("The hill climb minimum builder needs a non-null imporvement strategy to work with!");

            return this;
        }

        public double Build()
        {
            var minimum = double.MaxValue;

            for (var currentIteration = 1; currentIteration <= iterations; currentIteration++)
            {
                var randomState =
                    DomainHelper.RandomNumbersInDomainRange(function.GetDomain(), function.GetDimensionDefinition());

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
                        minimum = function.GetValue(improvement);
                    }
                }
            }

            return minimum;
        }

        private static IEnumerable<DimensionSet> GetNeighbourhood(DimensionSet subject)
        {
            return subject.SelectMany((vectorValue, index) =>
            {
                var bitRepresentation = BinaryRepresentation.FromDouble(vectorValue);
                var allAlterations = GetAlteredRepresentations(bitRepresentation);

                var neighbourhood = new List<DimensionSet>();
                foreach (var alteration in allAlterations)
                {
                    var neighbour = subject.ToList();

                    var decodedAlteration = DecodeBinaryRepresentation(alteration);
                    neighbour[index] = decodedAlteration;

                    neighbourhood.Add(new DimensionSet(neighbour));
                }

                return neighbourhood;
            }).ToList();
        }

        private static IEnumerable<BinaryRepresentation> GetAlteredRepresentations(BinaryRepresentation bitRepresentation)
        {
            var stringRepresentation = bitRepresentation.Value;

            var byteChunks = stringRepresentation.ChunksOfSize(8);

            return byteChunks.Select((chunk, index) =>
            {
                var chunkIndexStart = index * 8;

                var alteredBit = chunk[7] == '0' ? '1' : '0';

                var alteredChunk = new StringBuilder(chunk);
                alteredChunk[7] = alteredBit;
                    
                var alteredRepresentation = new StringBuilder(bitRepresentation.Value);
                alteredRepresentation.Remove(chunkIndexStart, 8);
                alteredRepresentation.Insert(chunkIndexStart, alteredChunk);

                return BinaryRepresentation.Create(alteredRepresentation.ToString());
            });

//            return stringRepresentation.Select((bit, index) =>
//            {
//                var alteredBit = bit == '0' ? '1' : '0';
//
//                var alteredRepresentation = new StringBuilder(bitRepresentation.Value);
//                alteredRepresentation[index] = alteredBit;
//
//                return BinaryRepresentation.Create(alteredRepresentation.ToString());
//            });
        }

        private static double DecodeBinaryRepresentation(BinaryRepresentation binaryRepresentation)
        {
            long v = 0;
            for (int i = binaryRepresentation.Value.Length - 1; i >= 0; i--) v = (v << 1) + (binaryRepresentation.Value[i] - '0');
            double d = BitConverter.ToDouble(BitConverter.GetBytes(v), 0);

            return d;

            /*var stringRepresentation = binaryRepresentation.Value;
            var representationLength = stringRepresentation.Length;

            var nBytes = (int)Math.Ceiling(representationLength / 8m);
            var bytesAsStrings =
                Enumerable.Range(0, nBytes)
                    .Select(i => stringRepresentation.Substring(8 * i, Math.Min(8, representationLength- 8 * i)));

            var byteArray = bytesAsStrings.Select(b => Convert.ToSByte(b)).ToArray();

            return Convert.ToDouble(byteArray); */
        }
    }
}