using System.Linq;
using GeneticAlgorithmsHomeworks.Function;

namespace GeneticAlgorithmsHomeworks.Homework2
{
    public class ChromosomeSetToDoubleSetConverter : FunctionSetToDoubleSetConverter<Chromosome>
    {
        public override DimensionSet<double> Convert(DimensionSet<Chromosome> source, DimensionalFunction function)
        {
            var doubles =
                source.Select((x, dimension) =>
                    BinaryHelper.DecodeBinary(x, function.GetDomain().GetDefinitionForDimension(dimension + 1), function.Precision));

            return new DimensionSet<double>(doubles);
        }
    }
}