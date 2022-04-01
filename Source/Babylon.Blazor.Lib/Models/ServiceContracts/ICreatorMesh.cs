using System.Threading.Tasks;
using Babylon.Blazor.Babylon;

namespace Babylon.Blazor.Models.ServiceContracts
{
    public interface ICreatorMesh
    {
        public Task<Mesh> CreateMesh();
    }
}