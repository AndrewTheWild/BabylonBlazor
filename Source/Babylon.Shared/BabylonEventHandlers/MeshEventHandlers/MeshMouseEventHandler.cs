using System;
using System.Threading.Tasks;
using Babylon.Model.MeshEvents;
using Microsoft.JSInterop;

namespace Babylon.Shared.BabylonEventHandlers.MeshEventHandlers
{
    public class MeshMouseEventHandler : MeshEventHandlerBase
    {
        public MeshMouseEventHandler(Func<Task> action) : base(action)
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