namespace Xamarin.FileStorage.Abstractions
{
    public interface IFileStorage
    {
        string Read(string i_FileName);

        T Read<T>(string i_FileName);
    }
}
