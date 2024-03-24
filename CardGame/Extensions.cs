using System.ComponentModel;
using System.Reflection;

namespace SamMRoberts.CardGame.Extensions
{
    public static class Common
    {
        public static bool IsPropertyReadOnly<T>(string PropertyName)
        {
            MemberInfo info = typeof(T).GetMember(PropertyName)[0];
            return Attribute.GetCustomAttribute(info, typeof(ReadOnlyAttribute)) is ReadOnlyAttribute attribute && attribute.IsReadOnly;
        }
    }
}