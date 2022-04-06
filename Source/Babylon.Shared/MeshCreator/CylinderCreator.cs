using System.Threading.Tasks;
using Babylon.Blazor.Babylon;
using Babylon.Blazor.Babylon.Parameters;
using Babylon.Blazor.Models.ServiceContracts;

namespace Babylon.Shared.MeshCreator
{
    public class CylinderCreator : ICreatorMesh
    {
        private readonly IBabylonInstance _babylonInstance;

        public Scene Scene { get; }

        public CylinderCreator(IBabylonInstance babylonInstance, Scene scene)
        {
            _babylonInstance = babylonInstance;
            Scene = scene;
        }

        public async Task<Mesh> CreateMesh(string name)
        {
            var cylinderOptions = new CylinderOptions { Diameter = 1, Height = 3 };
            var cylinderParameters = new MeshParameters(_babylonInstance) { Options = cylinderOptions };

            var tuple = Tools.RectangularTriangleSolutions(cylinderOptions.Height, Tools.GradToRadian(90));
            await cylinderParameters.SetRotation(15, 0, Tools.GradToRadian(90));

            await cylinderParameters.SetPosition(-tuple.X / 2, tuple.Y / 2, 0);

            return await Scene.CreateCylinder(name, cylinderParameters);
        }
    }
}