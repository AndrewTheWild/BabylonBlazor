using System;
using System.Threading.Tasks; 
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

        private DotNetObjectReference<MeshEventHandlerBase> _objRef;
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
                    return "registerOnPickTriggerForMesh";
                default:
                    Console.WriteLine("Please add function for register");
                    return string.Empty;
            }
        }

        public async Task AddEventHandler(Mesh mesh,ActionType actionType, MeshEventHandlerBase meshActionHandler)
        {  
            _objRef = DotNetObjectReference.Create(meshActionHandler);
            await mesh.BabylonInstance.InvokeAsync<string>(
                GetFuncNameByActionType(actionType), 
                Scene.JsObjRef, 
                mesh.JsObjRef, 
                _objRef);
        }

        public void Dispose()
        {
            _objRef?.Dispose();
        }
    }
}