using System;

namespace Babylon.Model.MeshEvents
{
    public abstract class MeshEventBase
    {
        protected readonly Action _action;

        public MeshEventBase(Action action)
            => _action = action;
    }
}