using System;
using Babylon.Model.MeshEvents;
using Microsoft.JSInterop;

namespace Babylon.Blazor.Babylon.Events.MeshEvents
{
    public class MeshMouseEvent: MeshEventBase
    { 
        public MeshMouseEvent(Action action) : base(action)
        {
        }


        // TODO: Create realization via interface or other way
        [JSInvokable]
        public void OnActionTrigger()
        {
            _action.Invoke();
        }
    }
}