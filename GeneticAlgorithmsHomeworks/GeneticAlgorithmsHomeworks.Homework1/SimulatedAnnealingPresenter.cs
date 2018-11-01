using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GeneticAlgorithmsHomeworks.Core;
using GeneticAlgorithmsHomeworks.Function;

namespace GeneticAlgorithmsHomeworks.Homework1
{
    public class SimulatedAnnealingPresenter : IHomeworkPresenter
    {
        public void Present()
        {
            var builder = new SimulatedAnnealingBinaryMinimumBuilder()
                .WithStartingTemperature(1);

            var functions = new List<DimensionalFunction>
            {
                new DeJong(),
                new Rastrigin(),
                new Schwefel(),
                new SixHump()
            };

            var numberOfExecutions = 30;
            var dimensions = new List<int> { 5, 10, 30 };

            foreach (var dimension in dimensions)
            {
                foreach (var dimensionalFunction in functions)
                {
                    var minimum = double.MaxValue;
                    var maximum = 0d;

                    var accumulated = 0d;

                    dimensionalFunction.TrySetDimension(dimension);
                    builder = builder.WithFunction(dimensionalFunction);
                    var values = new List<double>();

                    for (var i = 1; i <= numberOfExecutions; i++)
                    {
                        var currentValue = builder.Build();
                        if (currentValue < minimum)
                        {
                            minimum = currentValue;
                        }

                        if (currentValue > maximum)
                        {
                            maximum = currentValue;
                        }

                        accumulated += currentValue;
                        values.Add(currentValue);
                    }

                    var average = accumulated / numberOfExecutions;

                    Console.WriteLine($"Simulated annealing {dimensionalFunction} minimum for {dimension} dimensions: {minimum}");
                    Console.WriteLine($"Simulated annealing {dimensionalFunction} maximum for {dimension} dimensions: {maximum}");
                    Console.WriteLine($"Simulated annealing {dimensionalFunction} average for {dimension} dimensions: {average}");

                    var standardDeviation = Math.Sqrt(values.Sum(x => (x - average) * (x - average)) / (numberOfExecutions - 1));
                    Console.WriteLine($"Simulated annealing {dimensionalFunction} standard deviation for {dimension} dimensions: {standardDeviation}");
                    Console.WriteLine("---------------------");
                }
            }
        }

        private void DisplayMinimum(SimulatedAnnealingBinaryMinimumBuilder builder, DimensionalFunction function)
        {
            var minimumValue = builder.Build();
            Console.WriteLine($"{function} simulated annealing minimum: {minimumValue}");

            using (var file = new StreamWriter(@"SimulatedAnnealingHistory.txt", true))
            {
                file.WriteLine($"{function} simulated annealing minimum: {minimumValue}");
            }
        }
    }
}