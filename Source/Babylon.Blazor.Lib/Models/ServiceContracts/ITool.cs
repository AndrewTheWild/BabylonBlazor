using System.Collections.Generic;
using System.Threading.Tasks;
using Babylon.Blazor.Babylon; 

namespace Babylon.Blazor.Models.ServiceContracts
{
    public interface ITool
    {
        public Task Initialize(List<Mesh> meshes);
    }
}