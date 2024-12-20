using Scellecs.Morpeh.Addons.Systems;
using Unity.IL2CPP.CompilerServices;

namespace Scellecs.Morpeh.Addons.OneShot
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
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
            _registry?.Dispose();
        }
    }
}
