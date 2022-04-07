using System.Collections.Generic;
using System.Threading.Tasks;
using Babylon.Blazor.Babylon;
using Babylon.Blazor.Models.ServiceContracts;
using Babylon.Model.Constants;
using Babylon.Shared.Gizmo;

namespace Babylon.Shared.ToolsManager
{
    public class ToolManager
    {
        private readonly List<Mesh> _meshes;
        private readonly PositionGizmo _gizmo;

        public ToolManager(PositionGizmo gizmo,List<Mesh> meshes)
        {
            _meshes = meshes;
            _gizmo = gizmo;
        }

        public async Task AssignAction(TypeActions.Action action)
        {
            ITool tool;

            switch (action)
            {
                case TypeActions.Action.Move:
                    tool = new MoveTool(_gizmo);
                    break;
                default:
                    tool = new MoveTool(_gizmo);
                    break;
            }

           await tool.Initialize(_meshes);
        }
    }
}