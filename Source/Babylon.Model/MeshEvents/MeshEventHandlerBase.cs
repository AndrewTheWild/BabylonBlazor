using System;
using System.Threading.Tasks;

namespace Babylon.Model.MeshEvents
{
    public abstract class MeshEventHandlerBase
    {
        protected readonly Func<Task> _action;

        public MeshEventHandlerBase(Func<Task> action)
            => _action = action;
    }
}