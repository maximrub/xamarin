using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Xamarin.Extensions.Logging.Abstractions.Services
{
    /// <summary>
    /// Formatter to convert the named format items like {NamedformatItem} to <see cref="M:string.Format"/> format.
    /// </summary>
    public class LogValuesFormatter
    {
        private const string k_NullValue = "(null)";
        private static readonly object[] sr_EmptyArray = new object[0];
        private readonly string r_Format;
        private readonly List<string> r_ValueNames = new List<string>();

        public LogValuesFormatter(string format)
        {
            OriginalFormat = format;

            var sb = new StringBuilder();
            var scanIndex = 0;
            var endIndex = format.Length;

            while (scanIndex < endIndex)
            {
                var openBraceIndex = findBraceIndex(format, '{', scanIndex, endIndex);
                var closeBraceIndex = findBraceIndex(format, '}', openBraceIndex, endIndex);

                // Format item syntax : { index[,alignment][ :formatString] }.
                var formatDelimiterIndex = findIndexOf(format, ',', openBraceIndex, closeBraceIndex);
                if (formatDelimiterIndex == closeBraceIndex)
                {
                    formatDelimiterIndex = findIndexOf(format, ':', openBraceIndex, closeBraceIndex);
                }

                if (closeBraceIndex == endIndex)
                {
                    sb.Append(format, scanIndex, endIndex - scanIndex);
                    scanIndex = endIndex;
                }
                else
                {
                    sb.Append(format, scanIndex, openBraceIndex - scanIndex + 1);
                    sb.Append(r_ValueNames.Count.ToString(CultureInfo.InvariantCulture));
                    r_ValueNames.Add(format.Substring(openBraceIndex + 1, formatDelimiterIndex - openBraceIndex - 1));
                    sb.Append(format, formatDelimiterIndex, closeBraceIndex - formatDelimiterIndex + 1);

                    scanIndex = closeBraceIndex + 1;
                }
            }

            r_Format = sb.ToString();
        }

        public string OriginalFormat { get; private set; }

        public List<string> ValueNames => r_ValueNames;

        public string Format(object[] i_Values)
        {
            if (i_Values != null)
            {
                for (int i = 0; i < i_Values.Length; i++)
                {
                    var value = i_Values[i];

                    if (value == null)
                    {
                        i_Values[i] = k_NullValue;
                        continue;
                    }

                    // since 'string' implements IEnumerable, special case it
                    if (value is string)
                    {
                        continue;
                    }

                    // if the value implements IEnumerable, build a comma separated string.
                    IEnumerable enumerable = value as IEnumerable;
                    if (enumerable != null)
                    {
                        i_Values[i] = string.Join(", ", enumerable.Cast<object>().Select(o => o ?? k_NullValue));
                    }
                }
            }

            return string.Format(CultureInfo.InvariantCulture, r_Format, i_Values ?? sr_EmptyArray);
        }

        public KeyValuePair<string, object> GetValue(object[] i_Values, int i_Index)
        {
            if (i_Index < 0 || i_Index > r_ValueNames.Count)
            {
                throw new IndexOutOfRangeException(nameof(i_Index));
            }

            if (r_ValueNames.Count > i_Index)
            {
                return new KeyValuePair<string, object>(r_ValueNames[i_Index], i_Values[i_Index]);
            }

            return new KeyValuePair<string, object>("{OriginalFormat}", OriginalFormat);
        }

        public IEnumerable<KeyValuePair<string, object>> GetValues(object[] i_Values)
        {
            var valueArray = new KeyValuePair<string, object>[i_Values.Length + 1];
            for (int index = 0; index != r_ValueNames.Count; ++index)
            {
                valueArray[index] = new KeyValuePair<string, object>(r_ValueNames[index], i_Values[index]);
            }

            valueArray[valueArray.Length - 1] = new KeyValuePair<string, object>("{OriginalFormat}", OriginalFormat);

            return valueArray;
        }

        private static int findBraceIndex(string i_Format, char i_Brace, int i_StartIndex, int i_EndIndex)
        {
            // Example: {{prefix{{{Argument}}}suffix}}.
            var braceIndex = i_EndIndex;
            var scanIndex = i_StartIndex;
            var braceOccurenceCount = 0;

            while (scanIndex < i_EndIndex)
            {
                if (braceOccurenceCount > 0 && i_Format[scanIndex] != i_Brace)
                {
                    if (braceOccurenceCount % 2 == 0)
                    {
                        // Even number of '{' or '}' found. Proceed search with next occurence of '{' or '}'.
                        braceOccurenceCount = 0;
                        braceIndex = i_EndIndex;
                    }
                    else
                    {
                        // An unescaped '{' or '}' found.
                        break;
                    }
                }
                else if (i_Format[scanIndex] == i_Brace)
                {
                    if (i_Brace == '}')
                    {
                        if (braceOccurenceCount == 0)
                        {
                            // For '}' pick the first occurence.
                            braceIndex = scanIndex;
                        }
                    }
                    else
                    {
                        // For '{' pick the last occurence.
                        braceIndex = scanIndex;
                    }

                    braceOccurenceCount++;
                }

                scanIndex++;
            }

            return braceIndex;
        }

        private static int findIndexOf(string i_Format, char i_Ch, int i_StartIndex, int i_EndIndex)
        {
            int findIndex = i_Format.IndexOf(i_Ch, i_StartIndex, i_EndIndex - i_StartIndex);

            return findIndex == -1 ? i_EndIndex : findIndex;
        }
    }
}
