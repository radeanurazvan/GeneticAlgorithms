namespace GeneticAlgorithmsHomeworks.Function
{
    public abstract class FunctionSetToDoubleSetConverter<T>
    {
        public abstract DimensionSet<double> Convert(DimensionSet<T> source, DimensionalFunction function);
    }
}