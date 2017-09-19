using System.Linq;
using Missile.TextLauncher.Conversion;

namespace Missile.TextLauncher.Tests
{
    public class ConverterRepositoryBuilder
    {
        public ConverterRepositoryBuilder()
        {
            ConverterRepository = new ConverterRepository();
        }

        public ConverterRepository ConverterRepository { get; set; }

        public ConverterRepositoryBuilder WithConverter(IConverter converter)
        {
            ConverterRepository.Converters = ConverterRepository.Converters?.Concat(new[] {converter}).ToArray() ??
                                             new[] {converter};
            return this;
        }

        public ConverterRepository Build()
        {
            return ConverterRepository;
        }
    }
}