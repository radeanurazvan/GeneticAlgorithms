using System;
using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithmsHomeworks.Core;
using GeneticAlgorithmsHomeworks.Function;

namespace GeneticAlgorithmsHomeworks.Homework2
{
    public sealed class Population
    {
        public IEnumerable<DimensionSet<Chromosome>> Chromosomes { get; } = new List<DimensionSet<Chromosome>>();

        public int Size => Chromosomes.Count();

        private Population(IEnumerable<DimensionSet<Chromosome>> chromosomes)
        {
            this.Chromosomes = chromosomes ?? throw new InvalidOperationException();
        }

        public static Population Create(IEnumerable<DimensionSet<Chromosome>> chromosomes)
        {
            return new Population(chromosomes);
        }

        public Population Mutate(Rate mutationRate)
        {
            var mutatedChromosomes = Chromosomes.Select(set =>
            {
                return new DimensionSet<Chromosome>(set.Select(c => c.Mutate(mutationRate)));
            });
            return new Population(mutatedChromosomes);
        }

        public Population CrossOver(Rate crossoverRate)
        {
            var crossedSets = Chromosomes.Select(set =>
            {
                var chromosomesToCross = set.Where(c => crossoverRate.DoRandomPass()).ToList();
                if (chromosomesToCross.Any() && chromosomesToCross.Count() % 2 == 0)
                {
                    chromosomesToCross.Remove(chromosomesToCross.Last());
                }

                var crossedChromosomes = new List<Chromosome>();
                for (var i = 0; i < chromosomesToCross.Count - 1; i += 2)
                {
                    var firstChromosome = chromosomesToCross.ElementAt(i);
                    var secondChromosome = chromosomesToCross.ElementAt(i + 1);

                    var crossover = Crossover.Create(firstChromosome, secondChromosome);
                    crossedChromosomes.Add(crossover.FirstResult);
                    crossedChromosomes.Add(crossover.SecondResult);
                }

                var lefoutChromosomes = set.Except(chromosomesToCross);
                return new DimensionSet<Chromosome>(lefoutChromosomes.Concat(crossedChromosomes));
            });
            return new Population(crossedSets);
        }

        public Population Select(PopulationSelectionStrategy selectionStrategy, FitnessFunction fitness)
        {
            return selectionStrategy.Select(this, fitness);
        }
    }
}