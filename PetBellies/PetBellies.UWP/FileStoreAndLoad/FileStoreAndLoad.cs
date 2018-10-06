[assembly: Xamarin.Forms.Dependency(typeof(FileStoringWithDependency.UWP.FileStoreAndLoad.FileStoreAndLoad))]
namespace FileStoringWithDependency.UWP.FileStoreAndLoad
{
    public class FileStoreAndLoad : PetBellies.BLL.FileStoreAndLoad.IFileStoreAndLoad
    {
        public void SaveText(string filename, string text)
        {
            Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            var filePath = System.IO.Path.Combine(localFolder.Path, filename);
            System.IO.File.WriteAllText(filePath, text);
        }
        public string LoadText(string filename)
        {
            Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            var filePath = System.IO.Path.Combine(localFolder.Path, filename);
            return System.IO.File.ReadAllText(filePath);
        }
    }
}