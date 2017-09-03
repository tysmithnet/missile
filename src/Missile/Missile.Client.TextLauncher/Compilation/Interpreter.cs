using System;

namespace Missile.Client.TextLauncher.Compilation
{
    public class Interpreter : IInterpreter
    {
        public void Interpret(RootNode root)
        {

        }

    }

    public interface IConverter
    {
        
    }

    public interface IConverter<in TSource, out TDest> : IConverter
    {
        TDest Convert(TSource source);
    }

    public interface IConverterRepository
    {
        IConverter Get(Type source, Type dest);
    }
}
