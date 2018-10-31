using System.Collections.Generic;
using GeneticAlgorithmsHomeworks.Function;

namespace GeneticAlgorithmsHomeworks.Homework1.Improvement
{
    public abstract class ImprovementStrategy
    {
        public abstract DimensionSet PickImprovement(IEnumerable<DimensionSet> neighbourhood, DimensionalFunction function, double currentMinimum);
    }
}