using System;
using System.Threading.Tasks;
using Babylon.Blazor.Babylon.MeshEvent; 

namespace Babylon.Shared.BabylonEventHandlers.MeshEventHandlers
{
    public class MeshMouseEventHandler : MeshEventHandlerBase
    {
        public MeshMouseEventHandler(Func<Task> action) : base(action)
        {
        } 
    }
}