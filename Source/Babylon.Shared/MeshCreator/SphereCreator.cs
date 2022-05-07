using System.Drawing;
using System.Threading.Tasks; 
using Babylon.Blazor.Babylon;
using Babylon.Blazor.Babylon.Parameters;
using Babylon.Blazor.Models.ServiceContracts;

namespace Babylon.Shared.MeshCreator
{
    public class SphereCreator : ICreatorMesh
    {
        private readonly IBabylonInstance _babylonInstance;

        public Scene Scene { get; }

        public SphereCreator(IBabylonInstance babylonInstance, Scene scene)
        {
            _babylonInstance = babylonInstance;
            Scene = scene;
        }

        public async Task<Mesh> CreateMesh(string name)
        {
            var options = new SphereOptions { Diameter = 5.0 };
            var parameters = new MeshParameters(_babylonInstance) { Options = options };
            await parameters.SetPosition(5,10, 0); 
            var sphere = await Scene.CreateSphere(name, parameters);
            var diffuseColor = await _babylonInstance.CreateColor3(Color.Aqua); 
            var material = await Scene.CreateMaterial("material1", diffuseColor, null, 1.0);
            sphere.SetMaterial(material);

            return sphere;
        }
    }
}