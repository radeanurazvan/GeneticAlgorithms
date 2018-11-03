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

            var builder = new HillClimbingMinimumBuilder()
                .WithIterations(20);

            var numberOfExecutions = 30;

            foreach (var dimension in dimensions)
            {
                foreach (var dimensionalFunction in functions)
                {
                    var firstImprovementMinimum = double.MaxValue;
                    var firstImprovementMaximum = 0d;

                    var bestImprovementMinimum = double.MaxValue;
                    var bestImprovementMaximum = 0d;

                    var firstImprovementAccumulated = 0d;
                    var bestImprovementAccumulated = 0d;

                    dimensionalFunction.TrySetDimension(dimension);
                    builder = builder.WithFunction(dimensionalFunction);

                    var firstImprovementValues = new List<double>();
                    var bestImprovementValues = new List<double>();

                    for (var i = 1; i <= numberOfExecutions; i++)
                    {

                        var firstImprovementValue = builder.WithImprovementStrategy(new FirstImprovementStrategy()).Build();
                        var bestImprovementValue = builder.WithImprovementStrategy(new BestImprovementStrategy()).Build();

                        firstImprovementAccumulated += firstImprovementValue;
                        bestImprovementValue += bestImprovementValue;

                        firstImprovementValues.Add(firstImprovementValue);
                        bestImprovementValues.Add(bestImprovementValue);

                        firstImprovementMinimum = firstImprovementValue < firstImprovementMinimum
                            ? firstImprovementValue
                            : firstImprovementMinimum;
                        firstImprovementMaximum = firstImprovementValue > firstImprovementMaximum
                            ? firstImprovementValue
                            : firstImprovementMaximum;

                        bestImprovementMinimum = bestImprovementValue < bestImprovementMinimum
                            ? bestImprovementValue
                            : bestImprovementMinimum;
                        bestImprovementMaximum = bestImprovementValue > bestImprovementMaximum
                            ? bestImprovementValue
                            : bestImprovementMaximum;
                    }

                    var firstImprovementAverage = firstImprovementAccumulated/ numberOfExecutions;
                    var bestImprovementAverage = bestImprovementAccumulated / numberOfExecutions;

                    Console.WriteLine($"HillClimb {dimensionalFunction} First Improvement minimum for {dimension} dimensions: {firstImprovementMinimum}");
                    Console.WriteLine($"HillClimb {dimensionalFunction} First Improvement maximum for {dimension} dimensions: {firstImprovementMaximum}");
                    Console.WriteLine($"HillClimb {dimensionalFunction} First Improvement average for {dimension} dimensions: {firstImprovementAverage}");

                    var firstImprovementDeviation = Math.Sqrt(firstImprovementValues.Sum(x => (x - firstImprovementAverage) * (x - firstImprovementAverage)) / (numberOfExecutions - 1));
                    Console.WriteLine($"HillClimb {dimensionalFunction} First Improvement standard deviation for {dimension} dimensions: {firstImprovementDeviation}");
                    Console.WriteLine("---------------------");

                    Console.WriteLine($"HillClimb {dimensionalFunction} Best Improvement minimum for {dimension} dimensions: {bestImprovementMinimum}");
                    Console.WriteLine($"HillClimb {dimensionalFunction} Best Improvement maximum for {dimension} dimensions: {bestImprovementMaximum}");
                    Console.WriteLine($"HillClimb {dimensionalFunction} Best Improvement average for {dimension} dimensions: {bestImprovementAverage}");

                    var bestImprovementDeviation = Math.Sqrt(bestImprovementValues.Sum(x => (x - bestImprovementAverage) * (x - bestImprovementAverage)) / (numberOfExecutions - 1));
                    Console.WriteLine($"HillClimb {dimensionalFunction} Best Improvement standard deviation for {dimension} dimensions: {bestImprovementDeviation}");
                    Console.WriteLine("---------------------");
                }
            }
        }

        private void DisplayImprovement(HillClimbingMinimumBuilder builder, DimensionalFunction function)
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