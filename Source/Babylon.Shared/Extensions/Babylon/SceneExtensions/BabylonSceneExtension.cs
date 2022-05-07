using System.Threading.Tasks; 
using Babylon.Blazor.Babylon;
using Babylon.Shared.Gizmo;
using Babylon.Shared.UtilityLayer;
using Microsoft.JSInterop;

namespace Babylon.Shared.Extensions.Babylon.SceneExtensions
{
    public static class BabylonSceneExtension
    { 
        public static async Task<UtilityLayerRenderer> CreateUntilityLayerRenderer(this Scene scene)
        {
            var jsObj= await scene.BabylonInstance.InvokeAsync<IJSObjectReference>("createUtilityLayerRenderer", scene.JsObjRef);

            return new UtilityLayerRenderer(jsObj,scene.BabylonInstance);
        }

        public static async Task<GizmoManager> CreateGizmoManager(this Scene scene, Mesh mesh=null)
        {
            var jsObj = await scene.BabylonInstance.InvokeAsync<IJSObjectReference>("createGizmoManager",
                scene.JsObjRef,
                mesh);

            return new GizmoManager(jsObj, scene.BabylonInstance);
        }

    }
}
