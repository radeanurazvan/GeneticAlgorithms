using System.Linq;
using GeneticAlgorithmsHomeworks.Function;

namespace GeneticAlgorithmsHomeworks.Homework2
{
    using GeneticAlgorithmsHomeworks.Core;

    public class ChromosomeToDoubleSetConverter : FunctionSetToDoubleSetConverter<Chromosome>
    {
        public override DimensionSet<double> Convert(Chromosome source, DimensionalFunction function)
        {
            var doubles =
                source.Representations.Select((x, dimension) =>
                    BinaryHelper.DecodeBinary(x, function.GetDomain().GetDefinitionForDimension(dimension + 1), function.Precision));

            return new DimensionSet<double>(doubles);
        }
    }
}