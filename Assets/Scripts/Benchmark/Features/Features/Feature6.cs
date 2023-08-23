using Scellecs.Morpeh.Addons.Feature;

namespace Assets.Scripts.Benchmark.Features
{
    internal class Feature6 : UpdateFeature
    {
        private readonly int _eventsPerFrame;

        public Feature6(int eventsPerFrame)
        {
            _eventsPerFrame = eventsPerFrame;
        }

        protected override void Initialize()
        {
            AddSystem(new System6(_eventsPerFrame, false));
            RegisterEvent<EcsEvent6>();
            RegisterRequest<EcsRequest6>();
        }
    }
}
