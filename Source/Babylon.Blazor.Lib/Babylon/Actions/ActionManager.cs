using System;
using System.Threading.Tasks;
using Babylon.Blazor.Babylon.Events.MeshEvents;
using Microsoft.JSInterop;

namespace Babylon.Blazor.Babylon.Actions
{
    public class ActionManager:IDisposable
    {
        private DotNetObjectReference<MeshMouseEvents> _objRef;
        public Scene Scene { get; }

        public ActionManager(Scene scene)
        {
            Scene = scene;
        }

        public async Task AddEventHandler(Mesh mesh)
        {
            _objRef = DotNetObjectReference.Create(new MeshMouseEvents());
            await mesh.BabylonInstance.InvokeAsync<string>("registerOnClickForMesh", Scene.JsObjRef, mesh.JsObjRef, _objRef);
        }

        public void Dispose()
        {
            _objRef?.Dispose();
        }
    }
}