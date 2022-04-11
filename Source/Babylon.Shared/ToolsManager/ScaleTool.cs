using System.Threading.Tasks;
using Babylon.Blazor.Models.ServiceContracts;
using Babylon.Model.Constants;
using Babylon.Shared.Gizmo;

namespace Babylon.Shared.ToolsManager
{
    public class ScaleTool : ITool
    {
        private readonly GizmoManager _gizmoManager;

        public ScaleTool(GizmoManager gizmoManager)
        {
            _gizmoManager = gizmoManager;
        }

        public async Task Initialize()
        {
            await _gizmoManager.SetOperationForMesh(TypeActions.Action.Scale);
        }
    }
}