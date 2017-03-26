using System.IO;
using Newtonsoft.Json;
using Xamarin.FileStorage.Abstractions;

namespace Xamarin.FileStorage.iOS
{
    public class FileStorage : IFileStorage
    {
        public string Read(string i_FileName)
        {
            string content;

            byte[] data = read(i_FileName);
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

        public T Read<T>(string i_FileName)
        {
            string content = Read(i_FileName);
            T data = JsonConvert.DeserializeObject<T>(content);

            return data;
        }

        private byte[] read(string i_FileName)
        {
            byte[] content = File.ReadAllBytes(i_FileName);
            content = content.CleanByteOrderMark();

            return content;
        }
    }
}
