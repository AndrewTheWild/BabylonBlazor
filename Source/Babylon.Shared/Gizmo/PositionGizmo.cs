using System.Threading.Tasks;
using Babylon.Blazor.Babylon;
using Microsoft.JSInterop;

namespace Babylon.Shared.Gizmo
{
    public class PositionGizmo : BabylonObject
    {
        private readonly IJSInProcessObjectReference _babylonInstance;

        public PositionGizmo(IJSObjectReference jsObjRef, IJSInProcessObjectReference babylonInstance) : base(jsObjRef)
        {
            _babylonInstance = babylonInstance;
        }

        public async Task AttachMeshToGizmo(Mesh mesh)
        {
            await _babylonInstance.InvokeAsync<IJSObjectReference>("attachMeshToGizmo", JsObjRef, mesh.JsObjRef);
        }
    }
}