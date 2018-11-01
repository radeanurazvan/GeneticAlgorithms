using System.Collections.Generic;
using GeneticAlgorithmsHomeworks.Core;
using GeneticAlgorithmsHomeworks.Function;

namespace GeneticAlgorithmsHomeworks.Homework1.Improvement
{
    public abstract class ImprovementStrategy
    {
        public abstract DimensionSet<BinaryRepresentation> PickImprovement(
            IEnumerable<DimensionSet<BinaryRepresentation>> neighbourhood, 
            DimensionalFunction function, 
            double currentMinimum);


        public abstract DimensionSet<double> PickImprovement(
            IEnumerable<DimensionSet<double>> neighbourhood,
            DimensionalFunction function,
            double currentMinimum);
    }
}