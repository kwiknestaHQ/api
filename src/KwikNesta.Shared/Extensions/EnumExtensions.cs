using System.ComponentModel;

namespace KwikNesta.Shared.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets enum descriptions
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            if (Attribute.GetCustomAttribute(field!, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                return attribute.Description;
            }
            throw new ArgumentException("Item not found.", nameof(value));
        }

        public static bool TryParse<TEnum>(this string value, out TEnum? @enum) where TEnum : Enum
        {
            @enum = default;
            if(Enum.TryParse(typeof(TEnum), value, out var result))
            {
                @enum = (TEnum)result;
                return true;
            }

            return false;
        }
    }
}
