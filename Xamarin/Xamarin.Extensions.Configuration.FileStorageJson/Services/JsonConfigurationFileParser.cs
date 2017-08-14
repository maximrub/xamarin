using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Extensions.Configuration.Abstractions.Services;

namespace Xamarin.Extensions.Configuration.FileStorageJson.Services
{
    internal class JsonConfigurationFileParser
    {
        private readonly IDictionary<string, string> r_Data =
            new SortedDictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private readonly Stack<string> r_Context = new Stack<string>();
        private string m_CurrentPath;
        private JsonTextReader m_Reader;

        /// <exception cref="JsonReaderException">
        /// <paramref name="i_Content" /> is not valid JSON.
        /// </exception>
        public IDictionary<string, string> Parse(string i_Content)
        {
            r_Data.Clear();
            using(StringReader stringReader = new StringReader(i_Content))
            {
                using(m_Reader = new JsonTextReader(stringReader))
                {
                    m_Reader.DateParseHandling = DateParseHandling.None;
                    var jsonConfig = JObject.Load(m_Reader);
                    visitJObject(jsonConfig);
                }
            }

            return r_Data;
        }

        private void visitJObject(JObject i_JObject)
        {
            foreach(JProperty property in i_JObject.Properties())
            {
                enterContext(property.Name);
                visitProperty(property);
                exitContext();
            }
        }

        private void visitProperty(JProperty i_Property)
        {
            visitToken(i_Property.Value);
        }

        private void visitToken(JToken i_Token)
        {
            switch(i_Token.Type)
            {
                case JTokenType.Object:
                    visitJObject(i_Token.Value<JObject>());
                    break;
                case JTokenType.Array:
                    visitArray(i_Token.Value<JArray>());
                    break;
                case JTokenType.Integer:
                case JTokenType.Float:
                case JTokenType.String:
                case JTokenType.Boolean:
                case JTokenType.Bytes:
                case JTokenType.Raw:
                case JTokenType.Null:
                    visitPrimitive(i_Token);
                    break;
                default:
                    throw new FormatException(
                        $"Unsupported JSON Token: {m_Reader.TokenType} | {m_Reader.Path} | {m_Reader.LineNumber} | {m_Reader.LinePosition}");
            }
        }

        private void visitArray(JArray i_Array)
        {
            for(int index = 0; index < i_Array.Count; index++)
            {
                enterContext(index.ToString());
                visitToken(i_Array[index]);
                exitContext();
            }
        }

        private void visitPrimitive(JToken i_Data)
        {
            string key = m_CurrentPath;

            if(r_Data.ContainsKey(key))
            {
                throw new FormatException($"{key} - key is duplicated");
            }

            r_Data[key] = i_Data.ToString();
        }

        private void enterContext(string i_Context)
        {
            r_Context.Push(i_Context);
            m_CurrentPath = ConfigurationPath.Combine(r_Context.Reverse());
        }

        private void exitContext()
        {
            r_Context.Pop();
            m_CurrentPath = ConfigurationPath.Combine(r_Context.Reverse());
        }
    }
}