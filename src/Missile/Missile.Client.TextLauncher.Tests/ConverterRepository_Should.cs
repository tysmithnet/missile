using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Missile.Client.TextLauncher.Compilation;
using Xunit;

namespace Missile.Client.TextLauncher.Tests
{
    public class ConverterRepository_Should
    {
        public interface ICompanyMember
        {

        }

        public interface IPayable
        {

        }

        public interface IEmployee : ICompanyMember, IPayable
        {

        }

        public class Employee : IEmployee
        {

        }

        public class Manager : Employee
        {

        }

        public class Volunteer : ICompanyMember
        {

        }

        public class IntToIEmployeeConverter : IConverter<int, IEmployee>
        {
            public IObservable<IEmployee> Convert(IObservable<int> source)
            {
                throw new NotImplementedException();
            }
        }

        public class IntToManagerConverter : IConverter<int, Manager>
        {
            public IObservable<Manager> Convert(IObservable<int> source)
            {
                throw new NotImplementedException();
            }
        }

        public class StringToEmployeeConverter : IConverter<string, Employee>
        {
            public IObservable<Employee> Convert(IObservable<string> source)
            {
                throw new NotImplementedException();
            }
        }

        [Fact]
        public void Return_All_Valid_Converters()
        {
            var converters = new IConverter[]
            {
                new IntToManagerConverter(),
                new IntToIEmployeeConverter(),
                new StringToEmployeeConverter(),
            };

            ConverterRepository converterRepository = new ConverterRepository(converters);

            var results = converterRepository.Get(typeof(int), typeof(IEmployee));
            var expected = new[] { converters[1], converters[0] };

            results.Should().Equal(expected, "multiple converters can be returned if the types are compatible, but non compatible converters should not be returned, and they should be in order of closeness of match");
        }
    }
}
