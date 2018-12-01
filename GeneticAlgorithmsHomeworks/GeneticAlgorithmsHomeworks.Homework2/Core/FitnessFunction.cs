using System;
using GeneticAlgorithmsHomeworks.Function;

namespace GeneticAlgorithmsHomeworks.Homework2
{
    public class FitnessFunction
    {
        private Func<DimensionSet<Chromosome>, double> fitnessFunc;

        private FitnessFunction(Func<DimensionSet<Chromosome>, double> fitnessFunc)
        {
            this.fitnessFunc = fitnessFunc ?? throw new InvalidOperationException("Fitness function cannot be null!");
        }

        public static FitnessFunction Create(Func<DimensionSet<Chromosome>, double> fitnessFunc)
        {
            return new FitnessFunction(fitnessFunc);
        }

        public static FitnessFunction FromFunctionToMinimize(DimensionalFunction function)
        {
            return new FitnessFunction(cs => -1 * function.GetValue(cs, new ChromosomeSetToDoubleSetConverter()));
        }

        public double ValueFor(DimensionSet<Chromosome> set)
        {
            return this.fitnessFunc(set);
        }
    }
}