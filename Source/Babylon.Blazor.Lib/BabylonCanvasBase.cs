using System;
using System.Drawing;
using System.Threading.Tasks;

using Babylon.Blazor.Chemical;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Babylon.Blazor
{
    /// <summary>
    /// Class BabylonCanvasBase.
    /// Implements the <see cref="Microsoft.AspNetCore.Components.ComponentBase" />
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Components.ComponentBase" />
    public class BabylonCanvasBase : ComponentBase
    {
        private BabylonInstance _babylonInstance;

        private bool _reRender;

        /// <summary>
        /// Initializes the szene.
        /// </summary>
        /// <param name="babylonInstance">The babylon instance.</param>
        /// <param name="canvasId">The canvas identifier.</param>
        protected virtual async Task InitializeScene(BabylonInstance babylonInstance, string canvasId)
        {
            ChemicalData panelData;
            if (SceneData is ChemicalData)
            {
                panelData = (ChemicalData)SceneData;
                MoleculeCreator creator = new MoleculeCreator(babylonInstance, canvasId, panelData);
                if (panelData.Atoms.Count > 0)
                {
                    if (panelData.ShowErrorText && !String.IsNullOrEmpty(panelData.ErrorText))
                    {
                        await babylonInstance.DrawText(canvasId, panelData.ErrorText, Color.DarkRed);
                    }
                    else
                    {
                        await creator.CreateAsync(this);
                    }
                }
                else
                {
                    await babylonInstance.DrawText(canvasId, "Nothing to Draw", Color.DarkRed);
                }

            }
            else
            {
                await babylonInstance.DrawText(canvasId, "Scene data is null", Color.DarkRed); 
            }

            
        }

        /// <summary>
        /// on after render as an asynchronous operation.
        /// </summary>
        /// <param name="firstRender">Set to <c>true</c> if this is the first time <see cref="M:Microsoft.AspNetCore.Components.ComponentBase.OnAfterRender(System.Boolean)" /> has been invoked
        /// on this component instance; otherwise <c>false</c>.</param>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task" /> representing any asynchronous operation.</returns>
        /// <remarks>The <see cref="M:Microsoft.AspNetCore.Components.ComponentBase.OnAfterRender(System.Boolean)" /> and <see cref="M:Microsoft.AspNetCore.Components.ComponentBase.OnAfterRenderAsync(System.Boolean)" /> lifecycle methods
        /// are useful for performing interop, or interacting with values received from <c>@ref</c>.
        /// Use the <paramref name="firstRender" /> parameter to ensure that initialization work is only performed
        /// once.</remarks>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                //https://playground.babylonjs.com
                //https://doc.babylonjs.com/

                //Very interesting a part of code || need to optimize

                try
                {
                    Console.WriteLine($"-------##{nameof(OnAfterRenderAsync)} {nameof(firstRender)} : {firstRender} | {nameof(_reRender)} : {_reRender} ");

                    if (_babylonInstance != null)
                    {
                        Console.WriteLine($"-------Dispose call from {nameof(BabylonCanvasBase)}");
                        _babylonInstance.Dispose();
                        _babylonInstance = null;
                    }

                    _babylonInstance = await instanceCreator.CreateBabylonAsync();
                    //var scene = await BabylonInstance.CreateTestScene(canvasId);

                    await InitializeScene(BabylonInstance, CanvasId);

                    Console.WriteLine("------Creating instance--------");
                }
                catch (Exception ex)
                {
                    await JS.InvokeVoidAsync("console.log", $"***Exception:{ex}");
                }
                finally
                {
                    //it is not the best solution but quickly
                    _reRender = false;
                }
            }
        }

        /// <summary>
        /// Returns a flag to indicate whether the component should render.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        protected override bool ShouldRender()
        {
            //Console.WriteLine("*ShouldRender*");
            _reRender = true;
            return base.ShouldRender();
        }

        /// <summary>
        /// Gets or sets the canvas identifier.
        /// </summary>
        /// <value>The canvas identifier.</value>
        [Parameter]
        public string CanvasId { get; set; } = "babylon-canvas";


        /// <summary>
        /// Gets or sets the scene data.
        /// </summary>
        /// <value>The scene data.</value>
        [Parameter]
        public IData SceneData { get; set; }

        /// <summary>
        /// Gets or sets the idle rotation speed.
        /// </summary>
        /// <value>The idle rotation speed.</value>
        [Parameter]
        public double IdleRotationSpeed { get; set; } = 1;

        /// <summary>
        /// Gets or sets a value indicating whether [use automatic rotate].
        /// </summary>
        /// <value><c>true</c> if [use automatic rotate]; otherwise, <c>false</c>.</value>
        [Parameter]
        public bool UseAutoRotate { get; set; } = true;

        /// <summary>
        /// Gets the babylon instance.
        /// </summary>
        /// <value>The babylon instance.</value>
        protected BabylonInstance BabylonInstance
        {
            get
            {
                return _babylonInstance;
            }
        }

        [Inject]
        private InstanceCreator instanceCreator { get; set; }

        [Inject]
        private IJSRuntime JS { get; set; }

        //[CascadingParameter]
        //public Error Error { get; set; }
        //https://docs.microsoft.com/en-us/aspnet/core/blazor/fundamentals/handle-errors?view=aspnetcore-5.0&pivots=webassembly
    }
}
