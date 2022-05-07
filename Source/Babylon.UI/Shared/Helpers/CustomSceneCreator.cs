using System;
using System.Collections.Generic; 
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Babylon.Blazor;
using Babylon.Blazor.Babylon; 
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

        public GizmoManager GizmoManager { get; private set; }

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

            var cameraTarget = await BabylonInstance.CreateVector3(0, 0, 0); 
            double absolutMax = 10;
            var camera = await Scene.CreateArcRotateCamera("Camera", 3 * Math.PI / 2, 3 * Math.PI / 8, absolutMax * 3.6, cameraTarget, CanvasId);
            var hemisphericLightDirection = await BabylonInstance.CreateVector3(1, 1, 0);
            var light1 = await Scene.CreateHemispehericLight("light1", hemisphericLightDirection, 0.98);

            await Scene.CreateGround();

            GizmoManager = await Scene.CreateGizmoManager(); 

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

            var newName = GenerateNameForMesh(typeMesh);
            var mesh = await creatorMesh.CreateMesh(newName);

            await GizmoManager.AttachMesh(mesh);

            return mesh;
        }

        private string GenerateNameForMesh(TypeMesh.Mesh typeMesh)
        {
            var baseName = TypeMesh.GetNameForMesh(typeMesh);

            var pattern = $"^{baseName}(\\d+)\\Z";
            var regularExp = new Regex(pattern);

            var existingNumber = new List<int>();

            foreach (var mesh in Meshes)
            {
                if (regularExp.IsMatch(mesh.Name))
                {
                    var numberPart = mesh.Name.Replace($"{baseName}", "");
                    if (int.TryParse(numberPart, out int number))
                    {
                        existingNumber.Add(number);
                    }
                }
            }

            var newNumber = MissingNumberInSequence.GetMissingElements(existingNumber);

            return $"{baseName}{newNumber}";
        } 

        private async Task RunRender(BabylonCanvasBase canvas, ArcRotateCamera camera, Engine engine, Scene scene)
        {
            await BabylonInstance.RunRenderLoop(engine, scene);
        }
    }
}
