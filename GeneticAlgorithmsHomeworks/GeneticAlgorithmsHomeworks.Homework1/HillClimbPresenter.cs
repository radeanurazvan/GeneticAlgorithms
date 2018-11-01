using System;
using System.Collections.Generic;
using System.IO;
using GeneticAlgorithmsHomeworks.Core;
using GeneticAlgorithmsHomeworks.Function;
using GeneticAlgorithmsHomeworks.Homework1.Improvement;

namespace GeneticAlgorithmsHomeworks.Homework1
{
    public class HillClimbPresenter : IHomeworkPresenter
    {
        public void Present()
        {
            var builder = new HillClimbingBinaryMinimumBuilder()
                .WithIterations(20);

            var functions = new List<DimensionalFunction>
            {
                new DeJong(),
                new Rastrigin(),
                new Schwefel(),
                new SixHump()
            };

            foreach (var dimensionalFunction in functions)
            {
                builder = builder.WithFunction(dimensionalFunction);

                DisplayImprovement(builder, dimensionalFunction);
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