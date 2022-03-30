﻿using System;
using System.Drawing;
using System.Threading.Tasks;
using Babylon.Blazor;
using Babylon.Blazor.Babylon;
using Babylon.Blazor.Babylon.Actions;
using Babylon.Blazor.Babylon.Events.MeshEvents;
using Babylon.Blazor.Babylon.Parameters;
using Babylon.Shared.Extensions.Babylon.MeshExetension;
using Babylon.Shared.Extensions.Babylon.SceneExtensions;

namespace Babylon.UI.Shared.Helpers
{
    public class CustomSceneCreator : SceneCreator
    {
        public Engine Engine { get; private set; }
        public Scene Scene { get; private set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="SceneCreator"/> class.
        /// </summary>
        /// <param name="babylonInstance">The babylon instance.</param>
        /// <param name="canvasId">The canvas identifier.</param>
        public CustomSceneCreator(BabylonInstance babylonInstance, string canvasId)
            : base(babylonInstance, canvasId)
        {
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

            //await AddSphere(scene,-4.5,0,0);
            //await AddSphere(scene, -10, 0, 0);
            //await AddSphere(scene, -20, 0, 0);

            //await AddCylinder("cyl1", 0);
            //await AddCylinder(scene, "cyl2", 90);

           var box1= await AddBox1(Scene);

           //TODO: Rename class like EventHandler
           await box1.RegisterAction(ActionManager.ActionType.OnPickTrigger,new MeshMouseEvent(()=>Console.WriteLine("Test new functionality")));

           await AddThorus(Scene);

            var utilLayer = await Scene.CreateUntilityLayerRenderer();
            var gizmo = await utilLayer.CreatePositionGizmo();

            await RunRender(canvas, camera, Engine, Scene);
        }

        private async Task AddThorus(Scene scene)
        {
            TorusOptions options = new TorusOptions() { Diameter = 4, Tessellation = 20 };
            MeshParameters torusParameters = new MeshParameters(BabylonInstance) { Options = options };
            Mesh torus = await scene.CreateTorus("Torus1", torusParameters);
            var diffuseColor = await BabylonInstance.CreateColor3(Color.CadetBlue);
            var material = await scene.CreateMaterial("material2", diffuseColor, null, 1.0);
            torus.SetMaterial(material);
        }

        private async Task<Mesh> AddBox1(Scene scene)
        {
            BoxOptions.FaceColorsObj boxColors = await BabylonInstance.CreateFaceColors(Color.Chocolate);
            Options boxOptions = new BoxOptions { Size = 2, FaceColors = boxColors };
            MeshParameters boxParameters = new MeshParameters(BabylonInstance) { Options = boxOptions };
            await boxParameters.SetPosition(0, 0, 0);
            return await scene.CreateBox("Box1", boxParameters);
        }

        public async Task AddBox(string name)
        {
            BoxOptions.FaceColorsObj boxColors = await BabylonInstance.CreateFaceColors(Color.Blue);
            Options boxOptions = new BoxOptions { Height = 2, Width = 5.5, Depth = 0.5, FaceColors = boxColors };
            MeshParameters boxParameters = new MeshParameters(BabylonInstance) { Options = boxOptions };
            await boxParameters.SetPosition(10, 0, 0);
            Console.WriteLine($"Box {nameof(AddBox)}");
            await Scene.CreateBox(name, boxParameters);
        }

        private async Task AddCylinder(Scene scene, string name, double angle)
        {
            CylinderOptions cylinderOptions = new CylinderOptions { Diameter = 1, Height = 3 };
            MeshParameters cylinderParameters = new MeshParameters(BabylonInstance) { Options = cylinderOptions };
            //double diffAngle = 0;
            var tuple = Tools.RectangularTriangleSolutions(cylinderOptions.Height, Tools.GradToRadian(angle));
            await cylinderParameters.SetRotation(0, 0, Tools.GradToRadian(angle));
            // with this line cylinder zero point is cylinder center
            //await cylinderParameters.SetPosition(0, 0, 0);

            //with this line cylinder zero point is start of cylinder
            await cylinderParameters.SetPosition(-tuple.X / 2, tuple.Y / 2, 0);
            var cyl1 = await scene.CreateCylinder(name, cylinderParameters);
        }

        private async Task AddSphere(Scene scene, double x, double y, double z)
        {
            SphereOptions options = new SphereOptions { Diameter = 5.0 };
            MeshParameters parameters = new MeshParameters(BabylonInstance) { Options = options };
            await parameters.SetPosition(x, y, z);
            //rotate for correct viewing of text texture
            //await parameters.SetRotation(0, 0, Tools.GradToRadian(180));
            var sphere = await scene.CreateSphere("sphere1", parameters);
            var diffuseColor = await BabylonInstance.CreateColor3(Color.Aqua); //Brown,DarkRed
            var material = await scene.CreateMaterial("material1", diffuseColor, null, 1.0);
            sphere.SetMaterial(material);
        }

        private async Task RunRender(BabylonCanvasBase canvas, ArcRotateCamera camera, Engine engine, Scene scene)
        {
            //await camera.SetAutoRotate(canvas.UseAutoRotate, canvas.IdleRotationSpeed);
            await BabylonInstance.RunRenderLoop(engine, scene);
        }
    }
}
