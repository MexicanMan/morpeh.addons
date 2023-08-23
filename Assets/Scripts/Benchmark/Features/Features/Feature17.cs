using Scellecs.Morpeh.Addons.Feature;

namespace Assets.Scripts.Benchmark.Features
{
    internal class Feature17 : UpdateFeature
    {
        private readonly int _eventsPerFrame;

        public Feature17(int eventsPerFrame)
        {
            _eventsPerFrame = eventsPerFrame;
        }

        protected override void Initialize()
        {
            AddSystem(new System17(_eventsPerFrame, false));
            RegisterEvent<EcsEvent17>();
            RegisterRequest<EcsRequest17>();
        }
    }
}
