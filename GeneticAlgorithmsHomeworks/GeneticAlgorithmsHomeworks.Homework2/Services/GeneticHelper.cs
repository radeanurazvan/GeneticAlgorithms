using System;
using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithmsHomeworks.Function;

namespace GeneticAlgorithmsHomeworks.Homework2
{
    public static class GeneticHelper
    {
        public static Population GeneratePopulation(
            int populationSize,
            Domain domain, 
            DimensionDefinition dimensionDefinition, 
            int precision)
        {
            if (populationSize <= 0)
            {
                throw new InvalidOperationException("Population size should be greater than 0!");
            }

            var population = new List<DimensionSet<Chromosome>>();

            for (var size = 1; size <= populationSize; size++)
            {
                var chromosomes = 
                    DomainHelper.RandomDimensionalBinaryValueInDomainRange(domain, dimensionDefinition, precision)
                    .Select(Chromosome.Create);
                var chromosomeSet = new DimensionSet<Chromosome>(chromosomes);

                population.Add(chromosomeSet);
            }

            return Population.Create(population);
        }
    }
}