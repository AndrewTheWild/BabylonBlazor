using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Babylon.Blazor.Babylon;
using Babylon.Blazor.Babylon.Actions;
using Babylon.Blazor.Models.ServiceContracts;
using Babylon.Model.Constants;
using Babylon.Shared.BabylonEventHandlers.MeshEventHandlers;
using Babylon.Shared.Gizmo;

namespace Babylon.Shared.ToolsManager
{
    public class MoveTool:ITool
    {
        private readonly GizmoManager _gizmoManager;

        public MoveTool(GizmoManager gizmoManager)
        {
            _gizmoManager = gizmoManager;
        }

        public async Task Initialize()
        {
            await _gizmoManager.SetOperationForMesh(TypeActions.Action.Move); 
        }
    }
}