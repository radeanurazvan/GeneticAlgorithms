using System;
using GeneticAlgorithmsHomeworks.Core;
using GeneticAlgorithmsHomeworks.Function;
using GeneticAlgorithmsHomeworks.Homework1.Improvement;

namespace GeneticAlgorithmsHomeworks.Homework1
{
    public class HillClimbPresenter : IHomeworkPresenter
    {
        public void Present()
        {
            var minimum = new HillClimbingMinimumBuilder()
                .WithFunction(new DeJong())
                .WithIterations(1)
                .WithImprovementStrategy(new BestImprovementStrategy())
                .Build();

            Console.WriteLine(minimum);
        }
    }
}