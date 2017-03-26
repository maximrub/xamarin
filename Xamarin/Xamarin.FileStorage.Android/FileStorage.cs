using System.IO;
using Android.App;
using Android.Content;
using Newtonsoft.Json;
using Xamarin.FileStorage.Abstractions;

namespace Xamarin.FileStorage.Android
{
    public class FileStorage : IFileStorage
    {
        private readonly Context r_Context;

        public FileStorage()
        {
            r_Context = Application.Context;
        }

        /// <exception cref="IOException">Asset read exception.</exception>
        public string Read(string i_FileName)
        {
            string content;

            using (Stream asset = r_Context.Assets.Open(i_FileName))
            {
                using (StreamReader streamReader = new StreamReader(asset))
                {
                    content = streamReader.ReadToEnd();
                }
            }

            return content;
        }

        /// <exception cref="IOException">Asset read exception.</exception>
        public T Read<T>(string i_FileName)
        {
            string content = Read(i_FileName);
            T data = JsonConvert.DeserializeObject<T>(content);

            return data;
        }
    }
}
