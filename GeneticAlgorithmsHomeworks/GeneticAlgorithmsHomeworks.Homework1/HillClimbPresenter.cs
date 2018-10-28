using System;
using GeneticAlgorithmsHomeworks.Core;
using GeneticAlgorithmsHomeworks.Function;

namespace GeneticAlgorithmsHomeworks.Homework1
{
    public class HillClimbPresenter : IHomeworkPresenter
    {
        public void Present()
        {
            var minimum = new HillClimbingMinimumBuilder()
                .WithFunction(new DeJong())
                .WithIterations(5)
                .Build();

            Console.WriteLine(minimum);
        }
    }
}