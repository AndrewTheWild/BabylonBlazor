using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Babylon.Blazor.Babylon;
using Babylon.Blazor.Babylon.Actions;
using Babylon.Blazor.Models.ServiceContracts;
using Babylon.Shared.BabylonEventHandlers.MeshEventHandlers;
using Babylon.Shared.Gizmo;

namespace Babylon.Shared.ToolsManager
{
    public class MoveTool:ITool
    {
        private readonly PositionGizmo _gizmo;

        public MoveTool(PositionGizmo gizmo)
        {
            _gizmo = gizmo;
        }

        public async Task Initialize(List<Mesh> meshes)
        {
            Console.WriteLine($"{nameof(MoveTool.Initialize)} {nameof(meshes)}:{meshes.Count} ");

            foreach (var mesh in meshes)
            {
               await mesh.RegisterAction(ActionManager.ActionType.OnPickTrigger,
                    new MeshMouseEventHandler(async () => await _gizmo.AttachMeshToGizmo(mesh)));
            }
        }
    }
}