namespace GeneticAlgorithmsHomeworks.Function
{
    public class UniversalDomain : Domain
    {
        private readonly DomainDefinition definition;

        public UniversalDomain(double start, double end)
        {
            definition = new DomainDefinition(start, end);
        }

        public override DomainDefinition GetDefinitionForDimension(int dimensionDefinition)
        {
            return definition;
        }
    }
}