namespace Scellecs.Morpeh.Addons.OneShot
{
    internal class OneShotCleanSystem : ICleanupSystem
    {
        private OneShotRegistry _registry;

        public World World { get; set; }

        public void OnAwake()
        {
            _registry = OneShotRegistry.GetFor(World);
        }

        public void OnUpdate(float deltaTime)
        {
            _registry.CleanOneShots();
        }

        public void Dispose()
        {
            _registry.Dispose();
        }
    }
}
