using Babylon.UI.Shared.Helpers;
using Microsoft.AspNetCore.Components;

namespace Babylon.UI.Shared.Components.ListMeshes
{
    public partial class ListMeshes
    {
        [Parameter]
        public CustomSceneCreator SceneCreator { get; set; }
    }
}
