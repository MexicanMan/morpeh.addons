using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Feature.Events;
using Scellecs.Morpeh.Addons.Systems;

namespace Assets.Scripts.Benchmark.Features
{
    internal sealed class System4 : UpdateSystem
    {
        private readonly int _eventsCount;
        private readonly bool _usePool;

        public System4(int eventsCount, bool usePool)
        {
            _eventsCount = eventsCount;
            _usePool = usePool;
        }

        public override void OnAwake() { }

        public override void OnUpdate(float deltaTime)
        {
            for (int i = 0; i < _eventsCount / 2; i++)
            {
                var e = World.CreateEntity();
                e.AddComponent<EcsEvent4>();
                
                e = World.CreateEntity();
                e.AddComponent<EcsRequest4>();
            }
        }
    }
}
