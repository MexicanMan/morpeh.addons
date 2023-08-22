using Scellecs.Morpeh.Addons.Feature;

namespace Assets.Scripts.Benchmark.Features
{
    internal class Feature5 : UpdateFeature
    {
        private readonly int _eventsPerFrame;

        public Feature5(int eventsPerFrame)
        {
            _eventsPerFrame = eventsPerFrame;
        }

        protected override void Initialize()
        {
            AddSystem(new System5(_eventsPerFrame, true));
            RegisterEvent<EcsEvent5>();
        }
    }
}
