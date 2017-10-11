namespace Missile.Core
{
    /// <summary>
    ///     Represents an object that can be used to write information to various destinations
    /// </summary>
    public interface ILogger
    {
        void Information(string message);
    }
}