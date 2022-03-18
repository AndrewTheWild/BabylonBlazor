using System.Threading.Tasks;

namespace Babylon.UI.Pages
{
    public partial class BabylonUI
    {
        private async Task CreateBox()
        {
            await SceneCreator.AddBox("Box2");

        }
    }
}
