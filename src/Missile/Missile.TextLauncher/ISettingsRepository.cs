using System.Collections.Generic;

namespace Missile.TextLauncher
{
    public interface ISettingsRepository
    {
        T Get<T>() where T : ISettings;
        IEnumerable<ISettings> GetAll();
        void Save<T>() where T : ISettings;
    }
}