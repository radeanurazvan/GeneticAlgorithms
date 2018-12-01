namespace GeneticAlgorithmsHomeworks.Homework2
{
    public abstract class PopulationSelectionStrategy
    {
        public abstract Population Select(Population population, FitnessFunction fitness);
    }
}