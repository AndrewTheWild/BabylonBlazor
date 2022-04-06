namespace Babylon.Model.Constants
{
    public class TypeMesh
    {
        public enum Mesh
        {
            Box,
            Torus,
            Cylinder,
            Sphere
        }

        public static string GetNameForMesh(Mesh type) => type switch
        {
            Mesh.Box => "Box",
            Mesh.Torus => "Torus",
            Mesh.Cylinder => "Cylinder",
            Mesh.Sphere => "Sphere",
            _ => string.Empty
        };
    }
}