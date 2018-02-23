using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Starship.Core.Models.Abstracts;

namespace Starship.Core.Services.Interfaces
{
    public interface IFileAccessor
    {
        Task WriteSpaceObjectsToFileAsync(IEnumerable<BaseSpaceObject> spaceObjects, string filePath);

        Task<IEnumerable<BaseSpaceObject>> ReadSpaceObjectFromFileAsync(string filePath);
    }
}
