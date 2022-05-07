using System; 
using System.Threading.Tasks; 
using Babylon.Model.Constants;
using Babylon.Shared.ToolsManager;
using Babylon.UI.Shared.Helpers;
using Babylon.UI.Shared.Helpers.App;
using Microsoft.AspNetCore.Components;

namespace Babylon.UI.Shared.Components.Toolbars
{
    public partial class ToolbarOperationsBabylon:IDisposable
    {
        private ToolManager _toolManager; 

        [Inject]
        private AppState AppState { get; set; }

        [Parameter]
        public CustomSceneCreator SceneCreator { get; set; } 

        protected override void OnAfterRender(bool firstRender)
        {
            if(firstRender)
                return;

            _toolManager = new ToolManager(SceneCreator.GizmoManager);

            AppState.OnChange += _toolManager.AssignAction;
        } 

        public async Task AssignAction(TypeActions.Action action)
        {
            try
            {
                await AppState.SetOperation(action); 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void Dispose()
        {
            AppState.OnChange -= _toolManager.AssignAction;
        }
    }
}
