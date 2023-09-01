using UnityEngine.Scripting;

namespace Scellecs.Morpeh.Addons.OneShot
{
    [Preserve]
    public sealed class OneShotPlugin : IWorldPlugin
    {
        private SystemsGroup _systemsGroup;

        [Preserve]
        public OneShotPlugin() { }

        [Preserve]
        public void Initialize(World world)
        {
            _systemsGroup = world.CreateSystemsGroup();
            _systemsGroup.AddSystem(new OneShotCleanSystem());

            world.AddPluginSystemsGroup(_systemsGroup);
        }

        [Preserve]
        public void Deinitialize(World world)
        {
            world.RemoveSystemsGroup(_systemsGroup);
        }
    }
}
