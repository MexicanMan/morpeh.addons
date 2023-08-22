using Assets.Scripts.Benchmark.EventEntityPool.OldEntity;
using Scellecs.Morpeh.Addons.Feature;

namespace Assets.Scripts.Benchmark.EventEntityPool
{
    internal class OldEntityFeature : UpdateFeature
    {
        private readonly int _eventsPerFrame;

        public OldEntityFeature(int eventsPerFrame)
        {
            _eventsPerFrame = eventsPerFrame;
        }

        protected override void Initialize()
        {
            AddSystem(new BenchmarkSystem(_eventsPerFrame));
        }
    }
}
