using System;
using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithmsHomeworks.Core;

namespace GeneticAlgorithmsHomeworks.Homework3
{
    public sealed class TspPresenter : IHomeworkPresenter
    {
        public void Present()
        {
            var orchestrator = new TspOrchestrator()
                .WithBadGenerationsLimit(5000)
                .WithPopulationSize(50)
                .WithCrossoverRate(0.5)
                .WithMutationRate(0.15)
                .WithCrossover(new TspCrossover());
            var builder = new TspWinnerBuilder(orchestrator as TspOrchestrator);

            var numberOfExecutions = 30;
            var accumulated = 0d;
            var values = new List<TspChromosome>();

            var minimumDistance = double.MaxValue;
            var maximumDistance = double.MinValue;
            TspChromosome worstEver = null;
            TspChromosome bestEver = null;

            for (var i = 1; i <= numberOfExecutions; i++)
            {

                var bestPath = builder.Build();
                var bestPathDistance = bestPath.GetTravelDistance();

                accumulated += bestPathDistance;

                values.Add(bestPath);

                if (bestPathDistance < minimumDistance)
                {
                    minimumDistance = bestPathDistance;
                    bestEver = bestPath;
                }

                if (bestPathDistance > maximumDistance)
                {
                    maximumDistance = bestPathDistance;
                    worstEver = bestPath;
                }
            }

            var average = accumulated / numberOfExecutions;

            Console.WriteLine($"Genetic minimum path found {minimumDistance}: \n\t{bestEver.GetPath()}");
            Console.WriteLine($"Genetic maximum path found {maximumDistance}: \n\t{worstEver.GetPath()}");
            Console.WriteLine($"Genetic average path: {average}");

            var deviation = Math.Sqrt(values.Sum(x => Math.Pow(x.GetTravelDistance() - average, 2)) / (numberOfExecutions - 1));
            Console.WriteLine($"Genetic path standard deviation for: {deviation}");
            Console.WriteLine("---------------------");
      }
    }
}