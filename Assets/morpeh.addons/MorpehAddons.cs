using Scellecs.Morpeh.Addons.EntityPool;
using Scellecs.Morpeh.Addons.Feature;
using Scellecs.Morpeh.Addons.OneShot;

namespace Scellecs.Morpeh.Addons
{
    public static class MorpehAddons
    {
        public static void Initialize()
        {
            WorldPluginsExtensions.AddWorldPlugin(new FeaturePlugin());
            WorldPluginsExtensions.AddWorldPlugin(new OneShotPlugin());
            WorldPluginsExtensions.AddWorldPlugin(new EntityPoolPlugin());
        }
    }
}
