using System; 
using Microsoft.JSInterop;

namespace Babylon.Blazor.Babylon.Events.MeshEvents
{
    public class MeshMouseEvents
    {
        [JSInvokable]
        public void OnPickTrigger()
        {
            Console.WriteLine("On Click .NET");
        }
    }
}