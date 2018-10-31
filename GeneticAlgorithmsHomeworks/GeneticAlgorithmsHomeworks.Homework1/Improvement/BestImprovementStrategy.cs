using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithmsHomeworks.Function;

namespace GeneticAlgorithmsHomeworks.Homework1.Improvement
{
    public class BestImprovementStrategy : ImprovementStrategy 
    {
        public override DimensionSet PickImprovement(IEnumerable<DimensionSet> neighbourhood, DimensionalFunction function, double currentMinimum)
        {
            var bestImprovement = neighbourhood.First();

            foreach (var neighbour in neighbourhood)
            {
                var value = function.GetValue(neighbour);
                if (!(value < currentMinimum))
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