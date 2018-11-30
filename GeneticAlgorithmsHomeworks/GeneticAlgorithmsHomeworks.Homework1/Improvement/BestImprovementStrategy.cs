using System;
using System.Collections.Generic;
using GeneticAlgorithmsHomeworks.Core;
using GeneticAlgorithmsHomeworks.Function;

namespace GeneticAlgorithmsHomeworks.Homework1.Improvement
{
    public class BestImprovementStrategy : ImprovementStrategy 
    {
        public override DimensionSet<BinaryRepresentation> PickImprovement(
            IEnumerable<DimensionSet<BinaryRepresentation>> neighbourhood, 
            DimensionalFunction function, 
            double currentMinimum)
        {
            DimensionSet<BinaryRepresentation> bestImprovement = null;

            foreach (var neighbour in neighbourhood)
            {
                var value = function.GetValue(neighbour, new BinarySetToDoubleSetConverter());
                if (currentMinimum <= value)
                {
                    continue;
                }

                currentMinimum = value;
                bestImprovement = neighbour;
            }

            return bestImprovement;
        }

        public override DimensionSet<double> PickImprovement(
            IEnumerable<DimensionSet<double>> neighbourhood,
            DimensionalFunction function,
            double currentMinimum)
        {
            DimensionSet<double> bestImprovement = null;

            foreach (var neighbour in neighbourhood)
            {
                var value = function.GetValue(neighbour);
                if (currentMinimum <= value)
                {
                    continue;
                }

                currentMinimum = value;
                bestImprovement = neighbour;
            }

            return bestImprovement;
        }
    }
}