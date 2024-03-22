using System.ComponentModel;
using System.Reflection;

namespace SamMRoberts.CardGame.Extensions
{
    public static class Common
    {
        public static bool IsPropertyReadOnly<T>(string PropertyName)
        {
            MemberInfo info = typeof(T).GetMember(PropertyName)[0];

            ReadOnlyAttribute? attribute = Attribute.GetCustomAttribute(info, typeof(ReadOnlyAttribute)) as ReadOnlyAttribute;

            return (attribute != null && attribute.IsReadOnly);
        }
    }
}