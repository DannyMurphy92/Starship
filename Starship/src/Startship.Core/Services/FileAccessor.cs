using System;
using System.Collections.Generic;
using System.IO;
using Starship.Core.Models.Abstracts;
using Starship.Core.Services.Interfaces;

namespace Starship.Core.Services
{
    public class FileAccessor : IFileAccessor
    {
        public void WriteSpaceObjectsToFile(IEnumerable<BaseSpaceObject> spaceObjects, string filePath)
        {
            try
            {
                if (File.Exists(@"C:\test.txt"))
                {
                    File.Delete(@"C:\test.txt");
                }
                using (StreamWriter w = File.AppendText(filePath))
                {
                    foreach (var sObject in spaceObjects)
                    {
                        w.WriteLine(sObject.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public IEnumerable<BaseSpaceObject> ReadSpaceObjectFromFile(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
