using System.Threading.Tasks;
using Babylon.Blazor.Babylon;
using Babylon.Shared.Gizmo;
using Microsoft.JSInterop;

namespace Babylon.Shared.UtilityLayer
{
    public class UtilityLayerRenderer : BabylonObject
    {
        private IJSInProcessObjectReference _babylonInstance;

        public UtilityLayerRenderer(IJSObjectReference jsObjRef,IJSInProcessObjectReference babylonInstance) : base(jsObjRef)
        {
            _babylonInstance = babylonInstance;
        }

        public async Task<PositionGizmo> CreatePositionGizmo(Mesh attachedMesh=null)
        {
            var jsObj = await _babylonInstance.InvokeAsync<IJSObjectReference>("createPositionGizmo", JsObjRef, attachedMesh?.JsObjRef);
            return new PositionGizmo(jsObj);
        }
    }
}