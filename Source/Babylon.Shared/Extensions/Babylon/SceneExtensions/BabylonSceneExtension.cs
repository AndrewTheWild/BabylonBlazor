using System.Threading.Tasks; 
using Babylon.Blazor.Babylon;
using Microsoft.JSInterop;

namespace Babylon.Shared.Extensions.Babylon.SceneExtensions
{
    public static class BabylonSceneExtension
    {
        public static async Task<Mesh> GetMeshByName(this Scene scene,string name)
            =>await scene.BabylonInstance.InvokeAsync<Mesh>("getMeshByName", scene.JsObjRef, name); 
    }
}
