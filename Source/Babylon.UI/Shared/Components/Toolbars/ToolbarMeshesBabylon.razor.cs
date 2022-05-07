using System;
using System.Threading.Tasks;
using Babylon.Model.Constants;
using Babylon.UI.Shared.Helpers;
using Babylon.UI.Shared.Helpers.App;
using Microsoft.AspNetCore.Components;

namespace Babylon.UI.Shared.Components.Toolbars
{
    public partial class ToolbarMeshesBabylon
    {
        [Inject]
        private AppState AppState { get; set; }

        [Parameter]
        public CustomSceneCreator SceneCreator { get; set; }

        private async Task CreateMesh(TypeMesh.Mesh type)
        {  
            try
            {
               var mesh=await SceneCreator.CreateMesh(type); 

               SceneCreator.Meshes.Add(mesh);

               await AppState.NotifyStateChanged();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

    }
}
