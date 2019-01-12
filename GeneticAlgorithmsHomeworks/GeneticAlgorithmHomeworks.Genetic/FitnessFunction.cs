using System;

namespace GeneticAlgorithmsHomeworks.Genetic
{
    public class FitnessFunction<TChromosome, TGene>
        where TChromosome : AbstractChromosome<TGene, TChromosome>
    {
        private Func<TChromosome, double> fitnessFunc;

        private FitnessFunction(Func<TChromosome, double> fitnessFunc)
        {
            this.fitnessFunc = fitnessFunc ?? throw new InvalidOperationException("Fitness function cannot be null!");
        }

        public static FitnessFunction<TChromosome, TGene> Create(Func<TChromosome, double> fitnessFunc)
        {
            return new FitnessFunction<TChromosome, TGene>(fitnessFunc);
        }

        public double ValueFor(TChromosome chromosome)
        {
            return this.fitnessFunc(chromosome);
        }
    }
}