using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithmsHomeworks.Core;
using GeneticAlgorithmsHomeworks.Function;

namespace GeneticAlgorithmsHomeworks.Homework1.Improvement
{
    public class FirstImprovementStrategy : ImprovementStrategy
    {
        public override DimensionSet<BinaryRepresentation> PickImprovement(
            IEnumerable<DimensionSet<BinaryRepresentation>> neighbourhood, 
            DimensionalFunction function, 
            double currentMinimum)
        {
            return neighbourhood.FirstOrDefault(n => function.GetValue(n, new BinarySetToDoubleSetConverter()) < currentMinimum);
        }
        public override DimensionSet<double> PickImprovement(
            IEnumerable<DimensionSet<double>> neighbourhood,
            DimensionalFunction function,
            double currentMinimum)
        {
            return neighbourhood.FirstOrDefault(n => function.GetValue(n) < currentMinimum);
        }
    }
}