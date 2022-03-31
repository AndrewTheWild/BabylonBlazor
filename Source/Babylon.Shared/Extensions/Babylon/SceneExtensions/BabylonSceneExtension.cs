using System.Threading.Tasks; 
using Babylon.Blazor.Babylon;
using Babylon.Shared.UtilityLayer;
using Microsoft.JSInterop;

namespace Babylon.Shared.Extensions.Babylon.SceneExtensions
{
    public static class BabylonSceneExtension
    {
        //Not tested
        public static async Task<Mesh> GetMeshByName(this Scene scene,string name)
            =>await scene.BabylonInstance.InvokeAsync<Mesh>("getMeshByName", scene.JsObjRef, name);

        public static async Task<UtilityLayerRenderer> CreateUntilityLayerRenderer(this Scene scene)
        {
            var jsObj= await scene.BabylonInstance.InvokeAsync<IJSObjectReference>("createUtilityLayerRenderer", scene.JsObjRef);
            return new UtilityLayerRenderer(jsObj,scene.BabylonInstance);
        } 

    }
}
