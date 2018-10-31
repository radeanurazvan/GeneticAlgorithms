using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithmsHomeworks.Function;

namespace GeneticAlgorithmsHomeworks.Homework1.Improvement
{
    public class FirstImprovementStrategy : ImprovementStrategy
    {
        public override DimensionSet PickImprovement(IEnumerable<DimensionSet> neighbourhood, DimensionalFunction function, double currentMinimum)
        {
            return neighbourhood.FirstOrDefault(n => function.GetValue(n) < currentMinimum);
        }
    }
}