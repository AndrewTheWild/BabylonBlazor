using System.Threading.Tasks;
using Babylon.Blazor.Babylon;
using Babylon.Model.Constants;
using Microsoft.JSInterop;

namespace Babylon.Shared.Gizmo
{
    public class GizmoManager : BabylonObject
    {
        private readonly IJSInProcessObjectReference _babylonInstance;

        public GizmoManager(IJSObjectReference jsObjRef, IJSInProcessObjectReference babylonInstance) : base(jsObjRef)
        {
            _babylonInstance = babylonInstance;
        }

        public async Task AttachMesh(Mesh mesh)
        {
            await _babylonInstance.InvokeVoidAsync("attachMeshToGizmoManager", JsObjRef, mesh.JsObjRef);
        }

        public async Task SetOperationForMesh(TypeActions.Action action)
        {
            await _babylonInstance.InvokeVoidAsync("setOperationForGizmoManager", JsObjRef, (int)action);
        }
    }
}