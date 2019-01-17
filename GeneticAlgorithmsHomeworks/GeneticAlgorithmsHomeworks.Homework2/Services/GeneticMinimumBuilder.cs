using System;
using System.Linq;
using GeneticAlgorithmsHomeworks.Core;
using GeneticAlgorithmsHomeworks.Function;

namespace GeneticAlgorithmsHomeworks.Homework2
{
    using GeneticAlgorithmsHomeworks.Genetic;

    public sealed class GeneticMinimumBuilder : GeneticOrchestrator<Chromosome, BinaryRepresentation>
    {
        private DimensionalFunction optimizingFunction;

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

            this.optimizingFunction.Precision = precision;
            return this;
        }

        protected override FitnessFunction<Chromosome, BinaryRepresentation> GetFitness()
        {
            return FitnessFunction<Chromosome, BinaryRepresentation>.Create(c => this.optimizingFunction.GetValue(c, new ChromosomeToDoubleSetConverter()));
        }

        protected override Chromosome GetBestFromPopulation(Population<Chromosome, BinaryRepresentation> population)
        {
            var populationMinimum = population.Chromosomes.Min(c =>
                optimizingFunction.GetValue(c, new ChromosomeToDoubleSetConverter()));
            return population.Chromosomes.First(
                c => optimizingFunction.GetValue(c, new ChromosomeToDoubleSetConverter()) == populationMinimum);
        }

        protected override bool IsBetterCandidate(Chromosome chromosome, Chromosome winner)
        {
            var chromosomeValue = optimizingFunction.GetValue(chromosome, new ChromosomeToDoubleSetConverter());
            var winnerValue = optimizingFunction.GetValue(chromosome, new ChromosomeToDoubleSetConverter());

            return chromosomeValue < winnerValue;
        }

        public double Build()
        {
            var population = GeneticHelper.GeneratePopulation(
                populationSize, 
                optimizingFunction.GetDomain(),
                optimizingFunction.GetDimensionDefinition(), 
                this.optimizingFunction.Precision);

            var winner = this.GetWinner(population);
            return this.fitnessFunction.ValueFor(winner);
        }
    }
}