using System.Collections.Generic;
using GeneticAlgorithmsHomeworks.Core;

namespace GeneticAlgorithmsHomeworks.Homework2
{
    using System;
    using System.Linq;

    using GeneticAlgorithmsHomeworks.Genetic;

    public class Chromosome : AbstractChromosome<BinaryRepresentation, Chromosome>
    {
        private Chromosome(IEnumerable<BinaryRepresentation> representations)
        : base(representations)
        {
            this.Representations = representations ?? throw new InvalidOperationException("Chromosome representations cannot be null!");
        }
    
        public IEnumerable<BinaryRepresentation> Representations { get; }

        public static Chromosome Create(IEnumerable<BinaryRepresentation> representations)
        {
            return new Chromosome(representations);
        }

        public override Chromosome Mutate(Rate mutationRate)
        {
            var mutatedRepresentations = this.Representations.Select(r => 
            {
                var mutatedRepresentation = r.Bits.Select(bit => 
                {
                    var mutationResult = bit;

                    mutationRate.RunOnSuccessfulRandomPass(() =>
                    {
                        mutationResult = bit.Negate();
                    });

                    return mutationResult;
                });

                return BinaryRepresentation.Create(mutatedRepresentation);
            });

            return new Chromosome(mutatedRepresentations);
        }
    }
}