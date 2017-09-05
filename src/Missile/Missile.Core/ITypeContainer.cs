namespace Missile.Core
{
    public interface ITypeContainer
    {
        T Resolve<T>();
    }
}