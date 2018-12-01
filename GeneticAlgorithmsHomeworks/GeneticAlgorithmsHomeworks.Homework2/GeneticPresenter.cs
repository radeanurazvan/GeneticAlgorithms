﻿using System;
using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithmsHomeworks.Core;
using GeneticAlgorithmsHomeworks.Function;

namespace GeneticAlgorithmsHomeworks.Homework2
{
    public class GeneticPresenter : IHomeworkPresenter
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

            var dimensions = new List<int> { 5, 10, 30 };

            var builder = new GeneticMinimumBuilder()
                .WithGenerations(50)
                .WithPopulationSize(50)
                .WithCrossoverRate(0.3)
                .WithMutationRate(0.01)
                .WithPrecision(3);

            var numberOfExecutions = 30;

            foreach (var dimension in dimensions)
            {
                foreach (var dimensionalFunction in functions)
                {
                    var minimum = double.MaxValue;
                    var maximum = double.MinValue;
                    var accumulated = 0d;

                    dimensionalFunction.TrySetDimension(dimension);
                    builder = builder.WithOptimizingFunction(dimensionalFunction);

                    var values = new List<double>();

                    for (var i = 1; i <= numberOfExecutions; i++)
                    {

                        var value= builder.Build();

                        accumulated += value;

                        values.Add(value);

                        minimum = value < minimum
                            ? value
                            : minimum;
                        maximum = value > maximum
                            ? value
                            : maximum;
                    }

                    var average = accumulated/ numberOfExecutions;

                    Console.WriteLine($"Genetic {dimensionalFunction} minimum for {dimension} dimensions: {minimum}");
                    Console.WriteLine($"Genetic {dimensionalFunction} maximum for {dimension} dimensions: {maximum}");
                    Console.WriteLine($"Genetic {dimensionalFunction} average for {dimension} dimensions: {average}");

                    var deviation = Math.Sqrt(values.Sum(x => (x - average) * (x - average)) / (numberOfExecutions - 1));
                    Console.WriteLine($"Genetic {dimensionalFunction} standard deviation for {dimension} dimensions: {deviation}");
                    Console.WriteLine("---------------------");
                }
            }
        }
    }
}