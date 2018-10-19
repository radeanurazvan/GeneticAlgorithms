using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithmsHomeworks.Function
{
    public class DimensionalDomain : Domain
    {
        private DimensionalDomain() { }

        private DimensionDefinition dimension;
        private readonly IList<DomainDefinition> definitions = new List<DomainDefinition>();

        public static DimensionalDomain FromDimension(DimensionDefinition dimensionDefinition)
        {
            return new DimensionalDomain
            {
                dimension = dimensionDefinition
            };
        }

        public DimensionalDomain WithDefinition(DomainDefinition definition)
        {
            if (definitions.Count >= dimension)
            {
                throw new InvalidOperationException("Cannot declare any more definitions into this domain!");
            }

            definitions.Add(definition);

            return this;
        }

        public override DomainDefinition GetDefinitionForDimension(int dimensionDefinition)
        {
            return definitions.ElementAt(dimensionDefinition-1);
        }
    }
}