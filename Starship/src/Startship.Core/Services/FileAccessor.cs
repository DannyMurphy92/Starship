using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Threading.Tasks;
using Starship.Core.Factories.Interfaces;
using Starship.Core.Models.Abstracts;
using Starship.Core.Services.Interfaces;

namespace Starship.Core.Services
{
    public class FileAccessor : IFileAccessor
    {
        private readonly IBatchSpaceObjectFactory batchSpaceObjFactory;
        private readonly IFileSystem fileSystem;

        public FileAccessor(IBatchSpaceObjectFactory batchSpaceObjFactory, IFileSystem fileSystem)
        {
            this.batchSpaceObjFactory = batchSpaceObjFactory;
            this.fileSystem = fileSystem;
        }

        public async Task WriteSpaceObjectsToFileAsync(IEnumerable<BaseSpaceObject> spaceObjects, string filePath)
        {
            try
            {
                if (fileSystem.File.Exists(filePath))
                {
                    fileSystem.File.Delete(filePath);
                }
                using (StreamWriter w = fileSystem.File.AppendText(filePath))
                {
                    foreach (var sObject in spaceObjects)
                    {
                        await w.WriteLineAsync(sObject.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<IEnumerable<BaseSpaceObject>> ReadSpaceObjectFromFileAsync(string filePath)
        {
            if (!fileSystem.File.Exists(filePath))
            {
                throw new FileNotFoundException("Could not find file", filePath);
            }
            var result = new List<BaseSpaceObject>();
            using (StreamReader r = new StreamReader(filePath))
            {
                var line = string.Empty;
                while ((line = await r.ReadLineAsync()) != null)
                {
                    var spaceObj = batchSpaceObjFactory.CreateFromString(line);

                    if (spaceObj != null)
                    {
                        result.Add(batchSpaceObjFactory.CreateFromString(line));
                    }
                }
                return result;
            }
        }
    }
}
