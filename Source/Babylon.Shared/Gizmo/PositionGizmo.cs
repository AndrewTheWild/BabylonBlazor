using Babylon.Blazor.Babylon;
using Microsoft.JSInterop;

namespace Babylon.Shared.Gizmo
{
    public class PositionGizmo:BabylonObject
    {
        public PositionGizmo(IJSObjectReference jsObjRef) : base(jsObjRef) { }
    }
}