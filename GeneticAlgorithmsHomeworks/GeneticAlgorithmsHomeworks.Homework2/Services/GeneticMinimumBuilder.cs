using System;
using System.Linq;
using GeneticAlgorithmsHomeworks.Core;
using GeneticAlgorithmsHomeworks.Function;

namespace GeneticAlgorithmsHomeworks.Homework2
{
    public sealed class GeneticMinimumBuilder
    {
        private int generations;
        private int populationSize;
        private Rate crossoverRate;
        private Rate mutationRate;
        private DimensionalFunction optimizingFunction;
        private int precision;

        public GeneticMinimumBuilder WithGenerations(int generations)
        {
            if (generations <= 0)
            {
                throw new InvalidOperationException("Generations number should be greater than 0!");
            }

            this.generations = generations;
            return this;
        }

        public GeneticMinimumBuilder WithPopulationSize(int size)
        {
            if (size <= 0)
            {
                throw new InvalidOperationException("Population size should not be negative");
            }

            this.populationSize = size;
            return this;
        }

        public GeneticMinimumBuilder WithCrossoverRate(double rate)
        {
            if (rate <= 0)
            {
                throw new InvalidOperationException("Crossover rate should be higher than 0!");
            } 

            this.crossoverRate = Rate.Create(rate);
            return this;
        }

        public GeneticMinimumBuilder WithMutationRate(double rate)
        {
            if (rate <= 0)
            {
                throw new InvalidOperationException("Mutation rate should be higher than 0!");
            }

            this.mutationRate = Rate.Create(rate);
            return this;
        }

        public GeneticMinimumBuilder WithOptimizingFunction(DimensionalFunction function)
        {
            this.optimizingFunction = function ?? throw new InvalidOperationException("Function should not be null!");

            return this;
        }

        public GeneticMinimumBuilder WithPrecision(int precision)
        {
            if (precision < 0)
            {
                throw new InvalidOperationException("Precision should not be negative!");
            }

            this.precision = precision;
            return this;
        }

        public double Build()
        {
            var minimum = double.MaxValue;
            optimizingFunction.Precision = precision;

            var population = GeneticHelper.GeneratePopulation(
                populationSize, 
                optimizingFunction.GetDomain(),
                optimizingFunction.GetDimensionDefinition(), 
                precision);

            for (var generation = 1; generation <= this.generations; generation++)
            {
                population = population.Mutate(mutationRate);
                population = population.CrossOver(crossoverRate);
                population = population.Select(
                    new RouletteWheelSelectionStrategy(), 
                    FitnessFunction.FromFunctionToMinimize(optimizingFunction));

                var generationMinimum = population.Chromosomes.Min(c =>
                    optimizingFunction.GetValue(c, new ChromosomeSetToDoubleSetConverter()));
                if (generationMinimum < minimum)
                {
                    minimum = generationMinimum;
                }
            }

            return minimum;
        }
    }
}