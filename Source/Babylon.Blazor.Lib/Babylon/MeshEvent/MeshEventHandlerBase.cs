using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Babylon.Blazor.Babylon.MeshEvent
{
    public abstract class MeshEventHandlerBase
    {
        protected readonly Func<Task> _action;

        public MeshEventHandlerBase(Func<Task> action)
            => _action = action;

        [JSInvokable]
        public virtual async Task OnActionTrigger()
        {
            await _action.Invoke();
        }
    }
}