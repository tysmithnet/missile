namespace Missile.TextLauncher.Destination
{
    public interface IDestinationRepository
    {
        RegisteredDestination Get(string requestedDestinationName);
        void Add(RegisteredDestination destination);
    }
}