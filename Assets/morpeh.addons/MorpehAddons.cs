using Scellecs.Morpeh.Addons.EntityPool;
using Scellecs.Morpeh.Addons.Feature;
using Scellecs.Morpeh.Addons.OneShot;

namespace Scellecs.Morpeh.Addons
{
    public static class MorpehAddons
    {
        public static void Initialize()
        {
            WorldExtensions.AddWorldPlugin(new FeaturePlugin());
            WorldExtensions.AddWorldPlugin(new OneShotPlugin());
            WorldExtensions.AddWorldPlugin(new EntityPoolPlugin());
        }
    }
}
