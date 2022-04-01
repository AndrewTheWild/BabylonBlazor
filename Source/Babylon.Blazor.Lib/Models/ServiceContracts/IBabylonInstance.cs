using System.Drawing;
using System.Threading.Tasks;
using Babylon.Blazor.Babylon;
using Babylon.Blazor.Babylon.Parameters;

namespace Babylon.Blazor.Models.ServiceContracts
{
    public interface IBabylonInstance
    {
        public Task<BoxOptions.FaceColorsObj> CreateFaceColors(Color color);
        public Task<Vector3> CreateVector3(double x, double y, double z);
    }
}