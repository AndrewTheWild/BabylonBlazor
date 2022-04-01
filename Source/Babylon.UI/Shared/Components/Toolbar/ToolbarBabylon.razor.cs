using System;
using System.Threading.Tasks; 
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
                Console.WriteLine("Click");
                await SceneCreator.AddBox("Box2");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
