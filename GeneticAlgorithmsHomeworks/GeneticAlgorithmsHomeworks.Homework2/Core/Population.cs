using System;
using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithmsHomeworks.Core;
using GeneticAlgorithmsHomeworks.Function;

namespace GeneticAlgorithmsHomeworks.Homework2
{
    using System.Collections;

    public sealed class Population
    {
        public IEnumerable<Chromosome> Chromosomes { get; } = new List<Chromosome>();

        public int Size => Chromosomes.Count();

        private Population(IEnumerable<Chromosome> chromosomes)
        {
            this.Chromosomes = chromosomes ?? throw new InvalidOperationException();
        }

        public static Population Create(IEnumerable<Chromosome> chromosomes)
        {
            return new Population(chromosomes);
        }

        public Population Mutate(Rate mutationRate)
        {
            var mutatedChromosomes = Chromosomes.Select(c => c.Mutate(mutationRate));
            return new Population(mutatedChromosomes);
        }

        public Population CrossOver(Rate crossoverRate)
        {
            var chromosomesIndexToCross = this.Chromosomes
                .Select((chromosome, index) => (chromosome, index))
                .Where(c => crossoverRate.DoRandomPass())
                .Select((c, i) => i)
                .ToList();
            if (chromosomesIndexToCross.Any() && chromosomesIndexToCross.Count() % 2 != 0)
            {
                chromosomesIndexToCross.Remove(chromosomesIndexToCross.Last());
            }

            var crossedChromosomes = new List<Chromosome>();
            for (var i = 0; i < chromosomesIndexToCross.Count - 1; i += 2)
            {
                var firstChromosome = Chromosomes.ElementAt(i);
                var secondChromosome = Chromosomes.ElementAt(i + 1);

                var crossover = Crossover.Create(firstChromosome, secondChromosome);
                crossedChromosomes.Add(crossover.FirstResult);
                crossedChromosomes.Add(crossover.SecondResult);
            }

            var lefoutChromosomes = Chromosomes.Where((c, i) => !chromosomesIndexToCross.Contains(i));
            return new Population(lefoutChromosomes.Concat(crossedChromosomes));
        }

        public Population Select(PopulationSelectionStrategy selectionStrategy, FitnessFunction fitness)
        {
            return selectionStrategy.Select(this, fitness);
        }
    }
}