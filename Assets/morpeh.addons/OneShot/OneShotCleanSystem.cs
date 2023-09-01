using Scellecs.Morpeh.Addons.Systems;

namespace Scellecs.Morpeh.Addons.OneShot
{
    internal class OneShotCleanSystem : CleanupSystem
    {
        private OneShotRegistry _registry;
        public override void OnAwake()
        {
            _registry = OneShotRegistry.GetFor(World);
        }

        public override void OnUpdate(float deltaTime)
        {
            _registry.CleanOneShots();
        }

        public override void Dispose()
        {
            _registry.Dispose();
        }
    }
}
