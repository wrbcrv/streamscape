using System.ComponentModel;

namespace Api.Services
{
    public static class Extensions
    {
        static public string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());

            if (field == null)
            {
                return value.ToString();
            }

            var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                return attribute.Description;
            }

            return value.ToString();
        }
    }
}