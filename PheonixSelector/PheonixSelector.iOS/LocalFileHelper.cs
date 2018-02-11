using System;
using System.IO;
using PheonixSelector.Interfaces;
using PheonixSelector.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(LocalFileHelper))]
namespace PheonixSelector.iOS
{
    class LocalFileHelper : ILocalFileHelper
    {
        public string GetLocalFilePath(string fileName)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, fileName);
        }
    }
}