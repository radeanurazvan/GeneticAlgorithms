using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithmsHomeworks.Function;

namespace GeneticAlgorithmsHomeworks.Homework1.Improvement
{
    public class BestImprovementStrategy : ImprovementStrategy 
    {
        public override DimensionSet PickImprovement(IEnumerable<DimensionSet> neighbourhood, DimensionalFunction function, double currentMinimum)
        {
            DimensionSet bestImprovement = null;

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