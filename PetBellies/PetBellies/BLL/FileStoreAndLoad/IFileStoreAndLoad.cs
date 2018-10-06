using System;
using System.Collections.Generic;
using System.Text;

namespace PetBellies.BLL.FileStoreAndLoad
{
    public interface IFileStoreAndLoad
    {
        void SaveText(string filename, string text);

        string LoadText(string filename);
    }
}
