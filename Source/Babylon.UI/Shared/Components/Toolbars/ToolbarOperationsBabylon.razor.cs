using System;
using System.Threading.Tasks;
using Babylon.Model.Constants;
using Babylon.Shared.ToolsManager;
using Babylon.UI.Shared.Helpers;
using Microsoft.AspNetCore.Components;

namespace Babylon.UI.Shared.Components.Toolbars
{
    public partial class ToolbarOperationsBabylon
    {
        [Parameter]
        public CustomSceneCreator SceneCreator { get; set; }

        public async Task AssignAction(TypeActions.Action action)
        {
            try
            {
                var toolManager = new ToolManager(SceneCreator.Gizmo, SceneCreator.Meshes);
                await toolManager.AssignAction(action);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
