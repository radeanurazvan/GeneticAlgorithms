using System;
using System.Linq;
using System.Text;
using GeneticAlgorithmsHomeworks.Core;
using GeneticAlgorithmsHomeworks.Function;

namespace GeneticAlgorithmsHomeworks.Homework1
{
    public class SimulatedAnnealingMinimumBuilder
    {
        private readonly double alpha = 0.09;
        private DimensionalFunction function;
        private readonly double minimumTemperature = 0.001;
        private double temperature = 1;

        public SimulatedAnnealingMinimumBuilder WithStartingTemperature(double temperature)
        {
            if (temperature < 0)
            {
                throw new ArgumentException("Temperature should not be negative!");
            }

            this.temperature = temperature;
            return this;
        }

        public SimulatedAnnealingMinimumBuilder WithFunction(DimensionalFunction function)
        {
            this.function =
                function ?? throw new ArgumentNullException(
                    "The simulated annealing minimum builder needs a non-null function to work with!");
            return this;
        }

        public double Build()
        {
            var minimum = double.MaxValue;

            while (temperature > minimumTemperature)
            {
                var randomState =
                    DomainHelper.RandomNumbersInDomainRange(function.GetDomain(), function.GetDimensionDefinition());

                var neighbour = GetNeighbour(randomState);
                var neighbourValue = function.GetValue(neighbour);

                var randomNumber = new Random().Next();

                if (minimum > neighbourValue || (randomNumber % 1000 / 100) < Math.Exp((neighbourValue - minimum)/temperature))
                {
                    minimum = neighbourValue;
                }

                temperature *= alpha;
            }

            return minimum;
        }

        private static DimensionSet<double> GetNeighbour(DimensionSet<double> subject)
        {
            var randomDimension = new Random().Next(0, subject.Count() - 1);

            var bitRepresentation = BinaryRepresentation.FromDouble(subject.ElementAt(randomDimension));

            var alteredRepresentation = new StringBuilder(bitRepresentation);
            var randomBit = new Random().Next(0, alteredRepresentation.Length);
            alteredRepresentation[randomBit] = alteredRepresentation[randomBit] == '0' ? '1' : '0';

            var alteredDimension =
                DecodeBinaryRepresentation(BinaryRepresentation.Create(alteredRepresentation.ToString()));

            var neighbour = subject.ToList();
            neighbour[randomDimension] = alteredDimension;

            return new DimensionSet<double>(neighbour);
        }

        private static double DecodeBinaryRepresentation(BinaryRepresentation binaryRepresentation)
        {
            long v = 0;
            for (var i = binaryRepresentation.Value.Length - 1; i >= 0; i--)
            {
                v = (v << 1) + (binaryRepresentation.Value[i] - '0');
            }

            var d = BitConverter.ToDouble(BitConverter.GetBytes(v), 0);

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