namespace Missile.TextLauncher
{
    public interface IDestinationRepository
    {
        RegisteredDestination Get(string requestedDestinationName);
        void Add(RegisteredDestination destination);
    }
}