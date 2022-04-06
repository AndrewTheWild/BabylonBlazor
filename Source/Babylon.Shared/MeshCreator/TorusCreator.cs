using System.Drawing;
using System.Threading.Tasks;
using Babylon.Blazor.Babylon;
using Babylon.Blazor.Babylon.Parameters;
using Babylon.Blazor.Models.ServiceContracts;

namespace Babylon.Shared.MeshCreator
{
    public class TorusCreator:ICreatorMesh
    {
        private readonly IBabylonInstance _babylonInstance;

        public Scene Scene { get; }

        public TorusCreator(IBabylonInstance babylonInstance, Scene scene)
        {
            _babylonInstance = babylonInstance;
            Scene = scene;
        }

        public async Task<Mesh> CreateMesh(string name)
        {
            var options = new TorusOptions() { Diameter = 4, Tessellation = 20 };
            var torusParameters = new MeshParameters(_babylonInstance) { Options = options };

            var torus = await Scene.CreateTorus("Torus1", torusParameters);

            var diffuseColor = await _babylonInstance.CreateColor3(Color.CadetBlue);
            var material = await Scene.CreateMaterial("material2", diffuseColor, null, 1.0);
            torus.SetMaterial(material);

            return torus;
        }
    }
}