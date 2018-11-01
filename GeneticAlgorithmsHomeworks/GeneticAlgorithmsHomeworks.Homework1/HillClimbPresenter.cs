using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GeneticAlgorithmsHomeworks.Core;
using GeneticAlgorithmsHomeworks.Function;
using GeneticAlgorithmsHomeworks.Homework1.Improvement;

namespace GeneticAlgorithmsHomeworks.Homework1
{
    public class HillClimbPresenter : IHomeworkPresenter
    {
        public void Present()
        {
            var functions = new List<DimensionalFunction>
            {
                new DeJong(),
                new Rastrigin(),
                new Schwefel(),
                new SixHump()
            };

            var dimensions = new List<int>{ 5, 10, 30 };

            var builder = new HillClimbingBinaryMinimumBuilder()
                .WithIterations(20);

            var numberOfExecutions = 30;

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

                        var firstImprovementValue = builder.WithImprovementStrategy(new FirstImprovementStrategy()).Build();
                        var bestImprovementValue = builder.WithImprovementStrategy(new BestImprovementStrategy()).Build();

                        var currentValue = firstImprovementValue < bestImprovementValue ? firstImprovementValue : bestImprovementValue;

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

                    Console.WriteLine($"HillClimb {dimensionalFunction} minimum for {dimension} dimensions: {minimum}");
                    Console.WriteLine($"HillClimb {dimensionalFunction} maximum for {dimension} dimensions: {maximum}");
                    Console.WriteLine($"HillClimb {dimensionalFunction} average for {dimension} dimensions: {average}");

                    var standardDeviation = Math.Sqrt(values.Sum(x => (x - average) * (x - average)) / (numberOfExecutions - 1));
                    Console.WriteLine($"HillClimb {dimensionalFunction} standard deviation for {dimension} dimensions: {standardDeviation}");
                    Console.WriteLine("---------------------");
                }
            }
        }

        private void DisplayImprovement(HillClimbingBinaryMinimumBuilder builder, DimensionalFunction function)
        {
            var firstImprovementValue = builder
                .WithImprovementStrategy(new FirstImprovementStrategy())
                .Build();
            Console.WriteLine($"{function} first improvement: {firstImprovementValue}");

            using (var file = new StreamWriter(@"History.txt", true))
            {
                file.WriteLine($"{function} first improvement: {firstImprovementValue}");
            }

            var bestImprovementValue = builder
                .WithImprovementStrategy(new BestImprovementStrategy())
                .Build();
            Console.WriteLine($"{function} best improvement: {bestImprovementValue}");


            using (var file = new StreamWriter(@"History.txt", true))
            {
                file.WriteLine($"{function} best improvement: {bestImprovementValue}");
            }
        }
    }
}