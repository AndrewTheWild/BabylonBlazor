using Babylon.Blazor.Babylon;
using Microsoft.JSInterop;

namespace Babylon.Shared.Extensions.Babylon.MeshExetension
{
    public static class MeshExtension
    {
        public static async void Scaling(this Mesh obj)
        {
            await obj.BabylonInstance.InvokeVoidAsync("setScalingObj",obj);
        }
    }
}