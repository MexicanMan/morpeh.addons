using Scellecs.Morpeh.Addons.EntityPool.Systems;
using UnityEngine.Scripting;

namespace Scellecs.Morpeh.Addons.EntityPool
{
    [Preserve]
    public sealed class EntityPoolPlugin : IWorldPlugin
    {
        private SystemsGroup _systemsGroup;

        [Preserve]
        public EntityPoolPlugin() { }

        [Preserve]
        public void Initialize(World world)
        {
            EntityPoolRegistry.InitializeEntityPool(world);

            _systemsGroup = world.CreateSystemsGroup();
            _systemsGroup.AddSystem(new ReturnToPoolSystem(EntityPoolRegistry.GetFor(world)));

            world.AddPluginSystemsGroup(_systemsGroup);
        }

        [Preserve]
        public void Deinitialize(World world)
        {
            world.RemoveSystemsGroup(_systemsGroup);

            EntityPoolRegistry.GetFor(world).Dispose();
        }
    }
}
