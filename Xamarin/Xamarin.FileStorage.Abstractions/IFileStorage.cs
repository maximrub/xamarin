using System.Threading.Tasks;

namespace Xamarin.FileStorage.Abstractions
{
    public interface IFileStorage
    {
        Task<string> Read(string i_FileName);

        Task<T> Read<T>(string i_FileName);
    }
}
