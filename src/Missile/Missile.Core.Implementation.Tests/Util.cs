using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Missile.TextLauncher.Tests
{
    [ExcludeFromCodeCoverage]
    internal static class Util
    {
        public static string Serialize(this object o)
        {
            var serializer = new XmlSerializer(o.GetType());
            using (var mem = new MemoryStream())
            {
                serializer.Serialize(mem, o);
                mem.Position = 0;
                return Encoding.UTF8.GetString(mem.ToArray());
            }
        }

        public static Stream ToStream(this string s)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(s));
        }
    }
}