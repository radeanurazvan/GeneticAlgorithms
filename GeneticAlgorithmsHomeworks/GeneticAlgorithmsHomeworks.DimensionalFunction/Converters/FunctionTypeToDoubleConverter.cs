namespace GeneticAlgorithmsHomeworks.Function
{
    public abstract class FunctionSetToDoubleSetConverter<TSource>
    {
        public abstract DimensionSet<double> Convert(TSource source, DimensionalFunction function);
    }
}