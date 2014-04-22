using Orchard.UI.Resources;

namespace Quicklogons
{
    public class ResourceManifest : IResourceManifestProvider 
    {
        public void BuildManifests(ResourceManifestBuilder builder) 
        {
            var manifest = builder.Add();
            manifest.DefineStyle("Quicklogons").SetUrl("Styles.css");
        }
    }
}
