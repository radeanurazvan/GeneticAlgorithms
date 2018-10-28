using System;
using GeneticAlgorithmsHomeworks.Function;

namespace GeneticAlgorithmsHomeworks.Homework0
{
    public class HeuristicRandomMinimumBuilder
    {
        private int randomTriesCount;
        private double step;
        private DimensionalFunction function;

        public HeuristicRandomMinimumBuilder WithTries(int tries)
        {
            if (tries < 0)
            {
                throw new ArgumentException("The heuristic random minimum builder should try at least once!");
            }

            randomTriesCount = tries;

            return this;
        }

        public HeuristicRandomMinimumBuilder WithStep(double step)
        {
            this.step = step;

            return this;
        }


        public HeuristicRandomMinimumBuilder WithFunction(DimensionalFunction function)
        {
            this.function = function;

            return this;
        }

        public double Build()
        {
            var currentMinimum = double.MaxValue;

            for (var currentTry = 1.0; currentTry <= randomTriesCount; currentTry += step)
            {
                var functionParameters =
                    DomainHelper.RandomNumbersInDomainRange(function.GetDomain(), function.GetDimension());

                var functionValue = function.GetValue(functionParameters);

                currentMinimum = TestMinimum(currentMinimum, functionValue);
            }

            return currentMinimum;
        }

        private static double TestMinimum(double minimum, double currentValue)
        {
            return currentValue < minimum ? currentValue : minimum;
        }
    }
}