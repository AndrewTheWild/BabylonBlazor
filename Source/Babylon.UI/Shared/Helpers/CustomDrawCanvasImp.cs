using System;
using System.Threading.Tasks;
using Babylon.Blazor; 

namespace Babylon.UI.Shared.Helpers
{
    public class CustomDrawCanvasImp : BabylonCanvasBase
    {
        public CustomSceneCreator SceneCreator { get; private set; }
        /// <summary>
        /// Initializes the scene.
        /// </summary>
        /// <param name="babylonInstance">The babylon instance.</param>
        /// <param name="canvasId">The canvas identifier.</param>
        protected override async Task InitializeScene(BabylonInstance babylonInstance, string canvasId)
        {
            try
            {
                SceneCreator = new CustomSceneCreator(babylonInstance, canvasId);
                StateHasChanged();

                await SceneCreator.CreateAsync(this);
            }
            catch (Exception e)
            {
                Console.WriteLine(e); 
            }
        }
    }
}