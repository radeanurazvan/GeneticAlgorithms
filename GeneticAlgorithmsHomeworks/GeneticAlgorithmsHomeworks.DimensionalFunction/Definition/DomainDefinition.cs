using System;

namespace GeneticAlgorithmsHomeworks.Function
{
    public class DomainDefinition
    {
        public DomainDefinition(double start, double end)
        {
            if (start > end)
            {
                throw new InvalidOperationException("Domain start cannot be greater than end!");
            }

            Start = start;
            End = end;
        }

        public double Start { get; }

        public double End { get; }
    }
}