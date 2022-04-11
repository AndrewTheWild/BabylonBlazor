using System.Threading.Tasks;
using Babylon.Blazor.Models.ServiceContracts;
using Babylon.Model.Constants;
using Babylon.Shared.Gizmo;

namespace Babylon.Shared.ToolsManager
{
    public class ToolManager
    { 
        private readonly GizmoManager _gizmoManager;

        public ToolManager(GizmoManager gizmoManager)
        { 
            _gizmoManager = gizmoManager;
        }

        public async Task AssignAction(TypeActions.Action action)
        {
            ITool tool;

            switch (action)
            {
                case TypeActions.Action.Move:
                    tool = new MoveTool(_gizmoManager);
                    break;
                case TypeActions.Action.Rotate:
                    tool = new RotateTool(_gizmoManager);
                    break;
                case TypeActions.Action.Scale:
                    tool = new ScaleTool(_gizmoManager);
                    break;
                default:
                    tool = new NoneTool(_gizmoManager);
                    break;
            }

           await tool.Initialize();
        }
    }
}