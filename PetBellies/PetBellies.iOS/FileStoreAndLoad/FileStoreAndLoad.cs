﻿[assembly: Xamarin.Forms.Dependency(typeof(FileStoringWithDependency.iOS.FileStoreAndLoad.FileStoreAndLoad))]
namespace FileStoringWithDependency.iOS.FileStoreAndLoad
{
    public class FileStoreAndLoad : PetBellies.BLL.FileStoreAndLoad.IFileStoreAndLoad
    {
        public void SaveText(string filename, string text)
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var filePath = System.IO.Path.Combine(documentsPath, filename);
            System.IO.File.WriteAllText(filePath, text);
        }
        public string LoadText(string filename)
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var filePath = System.IO.Path.Combine(documentsPath, filename);
            return System.IO.File.ReadAllText(filePath);
        }
    }
}