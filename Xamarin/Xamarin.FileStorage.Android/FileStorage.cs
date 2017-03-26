using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
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
        public async Task<string> Read(string i_FileName)
        {
            string content;

            using (Stream asset = r_Context.Assets.Open(i_FileName))
            {
                using (StreamReader streamReader = new StreamReader(asset))
                {
                    content = await streamReader.ReadToEndAsync();
                }
            }

            return content;
        }

        /// <exception cref="IOException">Asset read exception.</exception>
        public async Task<T> Read<T>(string i_FileName)
        {
            string content = await Read(i_FileName);
            T data = JsonConvert.DeserializeObject<T>(content);

            return data;
        }
    }
}
