using Scellecs.Morpeh.Addons.Systems;
using Scellecs.Morpeh.Addons.Feature.Events;

namespace Assets.Scripts.Benchmark.EventEntityPool.PoolV1
{
    internal sealed class BenchmarkSystem : UpdateSystem
    {
        private readonly int _eventsPerFrame;

        public BenchmarkSystem(int eventsPerFrame)
        {
            _eventsPerFrame = eventsPerFrame;
        }

        public override void OnAwake() { }

        public override void OnUpdate(float deltaTime)
        {
            for (int i = 0; i < _eventsPerFrame; i++)
                World.CreateEventEntity<EcsEvent>();
        }
    }
}
