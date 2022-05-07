using Babylon.Blazor.Babylon;
using Babylon.UI.Shared.Helpers;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Babylon.UI.Shared.Components.ListMeshes
{
    public partial class ListMeshes
    {
        [Parameter]
        public CustomSceneCreator SceneCreator { get; set; }

        private async Task RemoveItem(Mesh mesh)
        {
            SceneCreator?.Meshes.Remove(mesh);

            await SceneCreator.Scene.RemoveMesh(mesh);
        }
    }
}
