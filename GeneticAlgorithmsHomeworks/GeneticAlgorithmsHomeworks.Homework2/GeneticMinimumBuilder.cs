using System;
using GeneticAlgorithmsHomeworks.Function;

namespace GeneticAlgorithmsHomeworks.Homework2
{
    public sealed class GeneticMinimumBuilder
    {
        private int generations;
        private int populationSize;
        private double crossoverRate;
        private double mutationRate;
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

            this.crossoverRate = rate;
            return this;
        }

        public GeneticMinimumBuilder WithMutationRate(double rate)
        {
            if (rate <= 0)
            {
                throw new InvalidOperationException("Mutation rate should be higher than 0!");
            }

            this.mutationRate = rate;
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
            optimizingFunction.Precision = precision;
            var minimum = double.MaxValue;

            var population = GeneticHelper.GeneratePopulation(
                this.populationSize, 
                optimizingFunction.GetDomain(),
                optimizingFunction.GetDimensionDefinition(), 
                this.precision);



            return minimum;
        }

        private double EvaluateChromosomes(DimensionSet<Chromosome> chromosomeSet)
        {
            return this.optimizingFunction.GetValue(chromosomeSet, new ChromosomeSetToDoubleSetConverter());
        }
    }
}