using Scellecs.Morpeh.Addons.EntityPool.v1;
using Scellecs.Morpeh.Addons.Feature;
using Scellecs.Morpeh.Addons.OneShot;
using Scellecs.Morpeh.Collections;
using Scellecs.Morpeh.Helpers.OneFrame;

namespace Scellecs.Morpeh.Addons
{
    public static class MorpehAddons
    {
        public static void Initialize()
        {
            WorldExtensions.AddWorldPlugin(new FeaturePlugin());
            WorldExtensions.AddWorldPlugin(new OneShotPlugin());
            WorldExtensions.AddWorldPlugin(new EntityPoolPlugin());
            World.plugins.Add(new OneFramePlugin());
        }
    }
}
