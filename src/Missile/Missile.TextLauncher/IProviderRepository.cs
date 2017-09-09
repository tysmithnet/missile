namespace Missile.TextLauncher
{
    public interface IProviderRepository
    {
        RegisteredProvider Get(string name);
        void Add(RegisteredProvider provider);
    }
}