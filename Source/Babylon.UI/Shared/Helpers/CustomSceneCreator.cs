using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Babylon.Blazor;
using Babylon.Blazor.Babylon; 
using Babylon.Blazor.Babylon.Parameters;
using Babylon.Blazor.Models.ServiceContracts;
using Babylon.Model.Constants;
using Babylon.Shared.Algorithms; 
using Babylon.Shared.Extensions.Babylon.SceneExtensions;
using Babylon.Shared.Gizmo;
using Babylon.Shared.MeshCreator;

namespace Babylon.UI.Shared.Helpers
{
    public class CustomSceneCreator : SceneCreator
    {
        public Engine Engine { get; private set; }
        public Scene Scene { get; private set; }

        public PositionGizmo Gizmo { get; private set; }

        public List<Mesh> Meshes { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SceneCreator"/> class.
        /// </summary>
        /// <param name="babylonInstance">The babylon instance.</param>
        /// <param name="canvasId">The canvas identifier.</param>
        public CustomSceneCreator(BabylonInstance babylonInstance, string canvasId)
            : base(babylonInstance, canvasId)
        {
            Meshes = new List<Mesh>();
        } 

        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="canvas">The canvas.</param>
        /// <returns>Task.</returns>
        public override async Task CreateAsync(BabylonCanvasBase canvas)
        {
            Engine = await BabylonInstance.CreateEngine(CanvasId, true);
            Scene = await Engine.CreateScene();
            //set rotation center
            var cameraTarget = await BabylonInstance.CreateVector3(0, 0, 0);
            //set camera
            // var camera = await scene.CreateArcRotateCamera("Camera", Math.PI / 2, Math.PI / 2, 10, cameraTarget, CanvasId);
            double absolutMax = 10;
            var camera = await Scene.CreateArcRotateCamera("Camera", 3 * Math.PI / 2, 3 * Math.PI / 8, absolutMax * 3.6, cameraTarget, CanvasId);
            var hemisphericLightDirection = await BabylonInstance.CreateVector3(1, 1, 0);
            var light1 = await Scene.CreateHemispehericLight("light1", hemisphericLightDirection, 0.98);

            var utilLayer = await Scene.CreateUntilityLayerRenderer();
            Gizmo = await utilLayer.CreatePositionGizmo();

            //var box1 = await AddBox1(Scene);
            //await box1.RegisterAction(ActionManager.ActionType.OnPickTrigger,
            //    new MeshMouseEventHandler(async () => await Gizmo.AttachMeshToGizmo(box1)));

            //var torus = await AddThorus(Scene);
            //await torus.RegisterAction(ActionManager.ActionType.OnPickTrigger, 
            //    new MeshMouseEventHandler(async () => await gizmo.AttachMeshToGizmo(torus)));

            //Meshes.AddRange(new []{torus});

            await RunRender(canvas, camera, Engine, Scene);
        }

        public async Task<Mesh> CreateMesh(TypeMesh.Mesh typeMesh)
        {
            ICreatorMesh creatorMesh;

            switch (typeMesh)
            {
                case TypeMesh.Mesh.Box:
                    creatorMesh = new BoxCreator(BabylonInstance, Scene);
                    break;
                case TypeMesh.Mesh.Sphere:
                    creatorMesh = new SphereCreator(BabylonInstance, Scene);
                    break;
                case TypeMesh.Mesh.Torus:
                    creatorMesh = new TorusCreator(BabylonInstance, Scene);
                    break;
                case TypeMesh.Mesh.Cylinder:
                    creatorMesh = new CylinderCreator(BabylonInstance, Scene);
                    break;
                default:
                    creatorMesh = new BoxCreator(BabylonInstance, Scene);
                    break;
            } 

            var newName=GenerateNameForMesh(typeMesh);  
            var mesh = await creatorMesh.CreateMesh(newName);

            return mesh;
        }

        private string GenerateNameForMesh(TypeMesh.Mesh typeMesh)
        {
            var baseName = TypeMesh.GetNameForMesh(typeMesh);

            var pattern = $"^{baseName}(\\d+)\\Z";
            var regulaExp = new Regex(pattern);

            var existingNumber = new List<int>();

            foreach (var mesh in Meshes)
            {
                if (regulaExp.IsMatch(mesh.Name))
                {
                    var numberPart = mesh.Name.Replace($"{baseName}", "");
                    if (int.TryParse(numberPart, out int number))
                    {
                        existingNumber.Add(number);
                    }
                }
            }

            var newNumber=MissingNumberInSequence.GetMissingElements(existingNumber);

            return $"{baseName}{newNumber}";
        } 

        private async Task<Mesh> AddBox1(Scene scene)
        {
            BoxOptions.FaceColorsObj boxColors = await BabylonInstance.CreateFaceColors(Color.Chocolate);
            Options boxOptions = new BoxOptions { Size = 2, FaceColors = boxColors };
            MeshParameters boxParameters = new MeshParameters(BabylonInstance) { Options = boxOptions };
            await boxParameters.SetPosition(0, 0, 0);
            return await scene.CreateBox("Box1", boxParameters);
        } 

        private async Task RunRender(BabylonCanvasBase canvas, ArcRotateCamera camera, Engine engine, Scene scene)
        { 
            await BabylonInstance.RunRenderLoop(engine, scene);
        }
    }
}
