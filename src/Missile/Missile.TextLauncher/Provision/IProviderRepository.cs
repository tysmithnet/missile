namespace Missile.TextLauncher.Provision
{
    public interface IProviderRepository
    {
        RegisteredProvider Get(string name);
        void Add(RegisteredProvider provider);
    }
}