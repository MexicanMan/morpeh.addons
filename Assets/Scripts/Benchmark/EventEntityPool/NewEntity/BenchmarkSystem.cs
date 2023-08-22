using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;

namespace Assets.Scripts.Benchmark.EventEntityPool.NewEntity
{
    internal sealed class BenchmarkSystem : UpdateSystem
    {
        private readonly int _eventsPerFrame;

        private Filter _events;

        public BenchmarkSystem(int eventsPerFrame)
        {
            _eventsPerFrame = eventsPerFrame;
        }

        public override void OnAwake()
        {
            _events = World.Filter.With<EcsEvent>().Build();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var e in _events)
                e.RemoveComponent<EcsEvent>();

            for (int i = 0; i < _eventsPerFrame; i++)
            {
                var e = World.CreateEntity();
                e.AddComponent<EcsEvent>();
            }
        }
    }
}
