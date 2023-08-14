using Scellecs.Morpeh;
using Scellecs.Morpeh.Collections;

namespace Scellecs.Morpeh.Addons.Feature.ClearSystems
{
    internal sealed class ClearFromStashSystem : ISystem
    {
        public World World { get; set; }

        private readonly FastList<Stash> _stashes;

        public ClearFromStashSystem(FastList<Stash> stashes)
        {
            _stashes = stashes;
        }

        public void OnAwake()
        {   
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var stash in _stashes)
                stash.RemoveAll();
        }

        public void Dispose()
        {
        }
    }
}
