using System;
using System.Threading.Tasks;
using Babylon.Blazor.Babylon.MeshEvent; 
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

        public async Task<ActionMesh> AddEventHandler(Mesh mesh,ActionType actionType, MeshEventHandlerBase meshActionHandler)
        {  
            _objRef = DotNetObjectReference.Create(meshActionHandler);
            var action=await mesh.BabylonInstance.InvokeAsync<IJSObjectReference>(
                GetFuncNameByActionType(actionType), 
                Scene.JsObjRef, 
                mesh.JsObjRef, 
                _objRef);

            return new ActionMesh(action);
        }

        public void Dispose()
        {
            _objRef?.Dispose();
        }
    }
}