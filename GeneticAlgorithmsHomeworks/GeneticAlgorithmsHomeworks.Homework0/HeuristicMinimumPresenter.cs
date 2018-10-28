using System;
using GeneticAlgorithmsHomeworks.Core;
using GeneticAlgorithmsHomeworks.Function;

namespace GeneticAlgorithmsHomeworks.Homework0
{
    public class HeuristicMinimumPresenter : IHomeworkPresenter
    {
        public void Present()
        {
            var minimumBuilder = new HeuristicRandomMinimumBuilder();

            var deJongValue = minimumBuilder
                .WithTries(100)
                .WithStep(0.01)
                .WithFunction(new DeJong())
                .Build();
            DisplayResult("DeJong", deJongValue);

            var schwefelValue = minimumBuilder
                .WithTries(150)
                .WithStep(0.001)
                .WithFunction(new Schwefel())
                .Build();
            DisplayResult("Schwefel", schwefelValue);

            var rastriginValue = minimumBuilder
                .WithTries(200)
                .WithStep(0.0001)
                .WithFunction(new Rastrigin())
                .Build();
            DisplayResult("Rastrigin", rastriginValue);

            var sixHumpValue = minimumBuilder
                .WithTries(250)
                .WithStep(0.0001)
                .WithFunction(new SixHump())
                .Build();
            DisplayResult("SixHump", sixHumpValue);
        }

        private static void DisplayResult(string strategyName, double value)
        {
            Console.WriteLine($"Today's minimum for {strategyName} is {value}");
        }
    }
}