using System;

namespace Xamarin.Extensions.Core.Extensions
{
    public static class StringExtensions
    {
        public static string Truncate(this string i_Value, int i_MaxLength)
        {
            return i_Value != null ? i_Value.Substring(0, Math.Min(i_Value.Length, i_MaxLength)) : null;
        }
    }
}
