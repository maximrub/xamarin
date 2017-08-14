using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Xamarin.Extensions.Logging.Abstractions.Services
{
    /// <summary>
    /// LogValues to enable formatting options supported by <see cref="M:string.Format"/>.
    /// This also enables using {NamedformatItem} in the format string.
    /// </summary>
    public class FormattedLogValues : IReadOnlyList<KeyValuePair<string, object>>
    {
        internal const int k_MaxCachedFormatters = 1024;
        private const string k_NullFormat = "[null]";
        private static readonly ConcurrentDictionary<string, LogValuesFormatter> sr_Formatters = new ConcurrentDictionary<string, LogValuesFormatter>();      
        private static int s_Count;       
        private readonly LogValuesFormatter r_Formatter;
        private readonly object[] r_Values;
        private readonly string r_OriginalMessage;

        // for testing purposes
        internal LogValuesFormatter Formatter => r_Formatter;

        public FormattedLogValues(string i_Format, params object[] i_Values)
        {
            if (i_Values?.Length != 0 && i_Format != null)
            {
                if (s_Count >= k_MaxCachedFormatters)
                {
                    if (!sr_Formatters.TryGetValue(i_Format, out r_Formatter))
                    {
                        r_Formatter = new LogValuesFormatter(i_Format);
                    }
                }
                else
                {
                    r_Formatter = sr_Formatters.GetOrAdd(
                        i_Format,
                        i_Value =>
                            {
                                Interlocked.Increment(ref s_Count);
                            return new LogValuesFormatter(i_Value);
                        });
                }
            }

            r_OriginalMessage = i_Format ?? k_NullFormat;
            r_Values = i_Values;
        }

        public KeyValuePair<string, object> this[int i_Index]
        {
            get
            {
                if (i_Index < 0 || i_Index >= Count)
                {
                    throw new IndexOutOfRangeException(nameof(i_Index));
                }

                if (i_Index == Count - 1)
                {
                    return new KeyValuePair<string, object>("{OriginalFormat}", r_OriginalMessage);
                }

                return r_Formatter.GetValue(r_Values, i_Index);
            }
        }

        public int Count
        {
            get
            {
                if (r_Formatter == null)
                {
                    return 1;
                }

                return r_Formatter.ValueNames.Count + 1;
            }
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            for (int i = 0; i < Count; ++i)
            {
                yield return this[i];
            }
        }

        public override string ToString()
        {
            if (r_Formatter == null)
            {
                return r_OriginalMessage;
            }

            return r_Formatter.Format(r_Values);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
