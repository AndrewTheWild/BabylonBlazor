using System.Threading.Tasks;
using Babylon.Blazor.Babylon.Actions;
using Babylon.Blazor.Babylon.MeshEvent;
using Microsoft.JSInterop;

namespace Babylon.Blazor.Babylon
{
    /// <summary>
    /// Class Mesh.
    /// Implements the <see cref="BabylonObject" />
    /// Implements the <see cref="IJsLibInstanceGetter" />
    /// </summary>
    /// <seealso cref="BabylonObject" />
    /// <seealso cref="IJsLibInstanceGetter" />
    public class Mesh : BabylonObject, IJsLibInstanceGetter
    {
        private readonly Scene _scene;
        private readonly ActionManager _actionManagerAction;

        public string Name
        {
            get => BabylonInstance.Invoke<string>("getMeshName", JsObjRef);
            set => BabylonInstance.Invoke<string>("setMeshName", JsObjRef, value);
        }

        /// <summary>
        /// Gets the babylon instance.
        /// </summary>
        /// <value>The babylon instance.</value>
        public IJSInProcessObjectReference BabylonInstance { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Mesh"/> class.
        /// </summary>
        /// <param name="jsObjRef">The js object reference.</param>
        /// <param name="babylonInstance">The babylon instance.</param>
        /// <param name="scene"></param>
        public Mesh(IJSObjectReference jsObjRef, IJSInProcessObjectReference babylonInstance, Scene scene)
            : base(jsObjRef)
        {
            _scene = scene;
            _actionManagerAction = new ActionManager(_scene);
            BabylonInstance = babylonInstance;
        }

        /// <summary>
        /// Sets the material.
        /// </summary>
        /// <param name="mat">The mat.</param>
        public void SetMaterial(Material mat)
        {
            BabylonInstance.InvokeAsync<object>("setMaterial", JsObjRef, mat.JsObjRef);
        }

        public async Task<ActionMesh> RegisterAction(ActionManager.ActionType actionType, MeshEventHandlerBase meshEvent)
        {  
           return await _actionManagerAction.AddEventHandler(this, actionType, meshEvent);
        }
    }
}
