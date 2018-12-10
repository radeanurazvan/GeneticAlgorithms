using System;
using System.Collections.Generic;
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

            var population = new List<Chromosome>();

            for (var size = 1; size <= populationSize; size++)
            {
                var chromosomeComponents =
                    DomainHelper.RandomDimensionalBinaryValueInDomainRange(domain, dimensionDefinition, precision);
                var chromosome = Chromosome.Create(chromosomeComponents);

                population.Add(chromosome);
            }

            return Population.Create(population);
        }
    }
}