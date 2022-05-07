using System;
using System.Threading.Tasks;
using Babylon.Model.Constants;
using Babylon.UI.Shared.Helpers;
using Microsoft.AspNetCore.Components;

namespace Babylon.UI.Shared.Components.Toolbar
{
    public partial class ToolbarBabylon
    {
        [Parameter]
        public CustomSceneCreator SceneCreator { get; set; }

        private async Task CreateBox()
        {  
            try
            {
               await SceneCreator.CreateMesh(TypeMesh.Mesh.Box); 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
