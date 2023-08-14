using UnityEngine.Scripting;

namespace Scellecs.Morpeh.Addons.Feature
{
    [Preserve]
    public sealed class FeaturePlugin : IWorldPlugin
    {
        [Preserve]
        public FeaturePlugin() { }

        [Preserve]
        public void Initialize(World world)
        {
            FeatureRegistry.InitializeFeatureRegistry(world);
        }

        [Preserve]
        public void Deinitialize(World world)
        {
            FeatureRegistry.GetFor(world).Dispose();
        }
    }
}
