using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticAlgorithmsHomeworks.Function;
using Microsoft.VisualBasic.CompilerServices;

namespace GeneticAlgorithmsHomeworks.Homework1
{
    public class HillClimbingMinimumBuilder
    {
        private int iterations = 1;
        private DimensionalFunction function; 

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

        public double Build()
        {
            var minimum = double.MaxValue;

            for (var currentIteration = 1; currentIteration <= iterations; currentIteration++)
            {
                var initialState =
                    DomainHelper.RandomNumbersInDomainRange(function.GetDomain(), function.GetDimension());
                var solution = function.GetValue(initialState);

                var neighbourhood = GetNeighbourhood(initialState);

            }

            return minimum;
        }

        private static IEnumerable<IEnumerable<double>> GetNeighbourhood(IEnumerable<double> subject)
        {
            return subject.SelectMany((vectorValue, index) =>
            {
                var bitRepresentation = BitConverter.DoubleToInt64Bits(vectorValue);
                var allAlterations = GetAlteredRepresentations(bitRepresentation);

                var neighbourhood = new List<IEnumerable<double>>();
                foreach (var alteration in allAlterations)
                {
                    var neighbour = subject.ToList();

                    var decodedAlteration = DecodeBinaryRepresentation(alteration);
                    neighbour[index] = decodedAlteration;

                    neighbourhood.Add(neighbour);
                }

                return neighbourhood;
            });
        }

        private static IEnumerable<long> GetAlteredRepresentations(long bitRepresentation)
        {
            var stringRepresentation = bitRepresentation.ToString();

            return stringRepresentation.Select((bit, index) =>
            {
                var alteredBit = bit == '0' ? '1' : '0';

                var alteredRepresentation = new StringBuilder(bitRepresentation.ToString());
                alteredRepresentation[index] = alteredBit;

                return Convert.ToInt64(alteredRepresentation.ToString());
            });
        }

        private static double DecodeBinaryRepresentation(long binaryRepresentation)
        {
            var stringRepresentation = binaryRepresentation.ToString();
            var representationLength = stringRepresentation.Length;

            var nBytes = (int)Math.Ceiling(representationLength / 8m);
            var bytesAsStrings =
                Enumerable.Range(0, nBytes)
                    .Select(i => stringRepresentation.Substring(8 * i, Math.Min(8, representationLength- 8 * i)));

            var byteArray = bytesAsStrings.Select(b => Convert.ToByte(b)).ToArray();

            return Convert.ToDouble(byteArray);
        }
    }
}