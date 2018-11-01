using System;
using System.Linq;
using System.Text;
using GeneticAlgorithmsHomeworks.Core;
using GeneticAlgorithmsHomeworks.Function;

namespace GeneticAlgorithmsHomeworks.Homework1
{
    public class SimulatedAnnealingBinaryMinimumBuilder
    {
        private readonly double alpha = 0.99;
        private DimensionalFunction function;
        private readonly double minimumTemperature = 0.001;
        private double startingTemperature = 1;

        private int precision = 3;

        public SimulatedAnnealingBinaryMinimumBuilder WithStartingTemperature(double temperature)
        {
            if (temperature < 0)
            {
                throw new ArgumentException("Temperature should not be negative!");
            }

            this.startingTemperature = temperature;
            return this;
        }

        public SimulatedAnnealingBinaryMinimumBuilder WithFunction(DimensionalFunction function)
        {
            this.function =
                function ?? throw new ArgumentNullException(
                    "The simulated annealing minimum builder needs a non-null function to work with!");
            return this;
        }

        public double Build()
        {
            var temperature = startingTemperature;
            function.Precision = precision;

            var minimum = double.MaxValue;
            var currentState =
                DomainHelper.RandomBinaryNumbersInDomainRange(function.GetDomain(), function.GetDimensionDefinition(), precision);

            while (temperature > minimumTemperature)
            {
                var neighbour = GetNeighbour(currentState);
                var neighbourValue = function.GetValue(neighbour);

                var randomNumber = new Random().NextDouble();

                if (minimum > neighbourValue || randomNumber < Math.Exp((neighbourValue - minimum) / temperature))
                {
                    minimum = neighbourValue;
                    currentState = neighbour;
                }

                temperature *= alpha;
            }

            return minimum;
        }

        private DimensionSet<BinaryRepresentation> GetNeighbour(DimensionSet<BinaryRepresentation> subject)
        {
            var randomDimension = new Random().Next(0, subject.Count() - 1);

            var alteredRepresentation = new StringBuilder(subject.ElementAt(randomDimension).Value);
            var randomBit = new Random().Next(0, alteredRepresentation.Length);
            alteredRepresentation[randomBit] = alteredRepresentation[randomBit] == '0' ? '1' : '0';

            var neighbour = subject.ToList();
            neighbour[randomDimension] = BinaryRepresentation.Create(alteredRepresentation.ToString());

            return new DimensionSet<BinaryRepresentation>(neighbour);
        }
    }
}