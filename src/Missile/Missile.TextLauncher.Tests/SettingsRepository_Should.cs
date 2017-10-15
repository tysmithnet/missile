using System;
using System.Runtime.Serialization;
using Xunit;

namespace Missile.TextLauncher.Tests
{
    public class SettingsRepository_Should
    {
        private class FooSettings : ISettings, ISerializable
        {
            public void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                throw new NotImplementedException();
            }
        }

        private class BarSettings : ISettings, ISerializable
        {
            public void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                throw new NotImplementedException();
            }
        }


        [Fact]
        public void Save_Serializable_Settings()
        {
            var settingsRepo =
                new SettingsRepository {AllSettings = new ISettings[] {new FooSettings(), new BarSettings()}};
            
        }
    }
}