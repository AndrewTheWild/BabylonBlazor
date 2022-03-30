using System;
using System.Threading.Tasks;
using Babylon.Blazor.Babylon.Events.MeshEvents;
using Babylon.Model.MeshEvents;
using Microsoft.JSInterop;

namespace Babylon.Blazor.Babylon.Actions
{
    public class ActionManager:IDisposable
    {
        public enum ActionType
        {
            OnPickTrigger
        }

        private DotNetObjectReference<IMeshActionHandler> _objRef;
        public Scene Scene { get; } 

        public ActionManager(Scene scene)
        {
            Scene = scene;
        }

        private string GetFuncNameByActionType(ActionType actionType)
        {
            switch (actionType)
            {
                case ActionType.OnPickTrigger:
                    return "registerOnClickForMesh";
                default:
                    Console.WriteLine("Add handler for action");
                    return string.Empty;
            }
        }

        public async Task AddEventHandler(Mesh mesh,ActionType actionType, IMeshActionHandler meshActionHandler)
        {  
            _objRef = DotNetObjectReference.Create(meshActionHandler);
            await mesh.BabylonInstance.InvokeAsync<string>("registerOnClickForMesh", Scene.JsObjRef, mesh.JsObjRef, _objRef);
        }

        public void Dispose()
        {
            _objRef?.Dispose();
        }
    }
}