using Scellecs.Morpeh.Addons.Systems;
using Scellecs.Morpeh.Collections;

namespace Scellecs.Morpeh.Addons.Feature.ClearSystems
{
    internal sealed class ClearFromStashFixedSystem : FixedUpdateSystem
    {
        private readonly FastList<IStash> _stashes;

        public ClearFromStashFixedSystem(FastList<IStash> stashes)
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
