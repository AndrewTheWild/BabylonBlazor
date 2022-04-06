using System.Drawing;
using System.Threading.Tasks; 
using Babylon.Blazor.Babylon;
using Babylon.Blazor.Babylon.Parameters; 
using Babylon.Blazor.Models.ServiceContracts;

namespace Babylon.Shared.MeshCreator
{
    public class BoxCreator:ICreatorMesh
    {
        private readonly IBabylonInstance _babylonInstance;

        public Scene Scene { get; }

        public BoxCreator(IBabylonInstance babylonInstance, Scene scene)
        {
            _babylonInstance = babylonInstance;
            Scene = scene;
        }
        
        public async Task<Mesh> CreateMesh(string name)
        {
            var boxColors = await _babylonInstance.CreateFaceColors(Color.Blue);
            var boxOptions = new BoxOptions { Height = 2, Width = 5.5, Depth = 0.5, FaceColors = boxColors };
            var boxParameters = new MeshParameters(_babylonInstance) { Options = boxOptions };
            await boxParameters.SetPosition(10, 0, 0); 

            return await Scene.CreateBox(name, boxParameters);
        } 
    }
}