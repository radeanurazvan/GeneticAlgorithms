using System;
using GeneticAlgorithmsHomeworks.Function;

namespace GeneticAlgorithmsHomeworks.Homework2
{
    public class FitnessFunction
    {
        private Func<Chromosome, double> fitnessFunc;

        private FitnessFunction(Func<Chromosome, double> fitnessFunc)
        {
            this.fitnessFunc = fitnessFunc ?? throw new InvalidOperationException("Fitness function cannot be null!");
        }

        public static FitnessFunction Create(Func<Chromosome, double> fitnessFunc)
        {
            return new FitnessFunction(fitnessFunc);
        }

        public static FitnessFunction FromFunctionToMinimize(DimensionalFunction function)
        {
            return new FitnessFunction(cs => -1 * function.GetValue(cs, new ChromosomeToDoubleSetConverter()));
        }

        public double ValueFor(Chromosome chromosome)
        {
            return this.fitnessFunc(chromosome);
        }
    }
}