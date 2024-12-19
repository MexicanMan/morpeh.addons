using Scellecs.Morpeh.Addons.Systems;
using Scellecs.Morpeh.Collections;
using Unity.IL2CPP.CompilerServices;

namespace Scellecs.Morpeh.Addons.Feature.ClearSystems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    internal sealed class ClearFromStashSystem : UpdateSystem
    {
        private readonly FastList<IStash> _stashes;

        public ClearFromStashSystem(FastList<IStash> stashes)
        {
            _stashes = stashes;
        }

        public override void OnAwake()
        {   
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var stash in _stashes)
                stash.RemoveAll();
        }
    }
}
