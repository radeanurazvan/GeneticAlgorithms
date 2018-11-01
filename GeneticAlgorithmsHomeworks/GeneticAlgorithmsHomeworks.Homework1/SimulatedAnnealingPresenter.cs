using System;
using System.Collections.Generic;
using System.IO;
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

            foreach (var dimensionalFunction in functions)
            {
                builder = builder.WithFunction(dimensionalFunction);

                DisplayMinimum(builder, dimensionalFunction);
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