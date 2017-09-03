namespace Missile.Client.TextLauncher.Compilation
{
    public interface IConverter
    {
        object Convert(object source);
    }

    public interface IConverter<in TSource, out TDest> : IConverter
    {
        TDest Convert(TSource source);
    }
}