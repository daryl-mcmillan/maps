using System.Xml.Linq;

namespace maps
{
    public static class XElementExtensions {

        public static double GetAttributeDouble( this XElement element, string attributeName ) {
            var val = element.Attribute( attributeName ).Value;
            return double.Parse( val );
        }

        public static long GetAttributeLong( this XElement element, string attributeName ) {
            var val = element.Attribute( attributeName ).Value;
            return long.Parse( val );
        }

        public static string GetAttribute( this XElement element, string attributeName ) {
            var val = element.Attribute( attributeName ).Value;
            return val;
        }

    }
}