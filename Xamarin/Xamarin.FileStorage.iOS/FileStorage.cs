using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.FileStorage.Abstractions;

namespace Xamarin.FileStorage.iOS
{
    public class FileStorage : IFileStorage
    {
        public async Task<string> Read(string i_FileName)
        {
            string content;

            byte[] data = await read(i_FileName);
            if(data == null)
            {
                content = string.Empty;
            }
            else
            {
                content = System.Text.Encoding.UTF8.GetString(data);
            }

            return content;
        }

        public async Task<T> Read<T>(string i_FileName)
        {
            string content = await Read(i_FileName);
            T data = JsonConvert.DeserializeObject<T>(content);

            return data;
        }

        private Task<byte[]> read(string i_FileName)
        {
            byte[] content = File.ReadAllBytes(i_FileName);
            content = content.CleanByteOrderMark();

            return Task.FromResult(content);
        }
    }
}
