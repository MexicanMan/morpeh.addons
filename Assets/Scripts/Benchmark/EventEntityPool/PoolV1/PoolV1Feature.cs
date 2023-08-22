using Assets.Scripts.Benchmark.EventEntityPool.PoolV1;
using Scellecs.Morpeh.Addons.Feature;

namespace Assets.Scripts.Benchmark.EventEntityPool
{
    internal class PoolV1Feature : UpdateFeature
    {
        private readonly int _eventsPerFrame;

        public PoolV1Feature(int eventsPerFrame)
        {
            _eventsPerFrame = eventsPerFrame;
        }

        protected override void Initialize()
        {
            AddSystem(new BenchmarkSystem(_eventsPerFrame));
            RegisterEvent<EcsEvent>();
        }
    }
}
