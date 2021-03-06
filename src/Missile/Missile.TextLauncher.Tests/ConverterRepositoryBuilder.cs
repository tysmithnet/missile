﻿using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Missile.TextLauncher.Conversion;

namespace Missile.TextLauncher.Tests
{
    [ExcludeFromCodeCoverage]
    public class ConverterRepositoryBuilder
    {
        public ConverterRepositoryBuilder()
        {
            ConverterRepository = new ConverterRepository();
            ConverterRepository.ConverterSelectionStrategy = new ConverterSelectionStrategy();
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