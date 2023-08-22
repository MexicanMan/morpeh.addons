using Assets.Scripts.Benchmark.EventEntityPool.NewEntity;
using Scellecs.Morpeh.Addons.Feature;

namespace Assets.Scripts.Benchmark.EventEntityPool
{
    internal class NewEntityFeature : UpdateFeature
    {
        private readonly int _eventsPerFrame;

        public NewEntityFeature(int eventsPerFrame)
        {
            _eventsPerFrame = eventsPerFrame;
        }

        protected override void Initialize()
        {
            AddSystem(new BenchmarkSystem(_eventsPerFrame));
        }
    }
}
