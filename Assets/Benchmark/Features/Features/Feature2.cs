using Scellecs.Morpeh.Addons.Feature;

namespace Assets.Scripts.Benchmark.Features
{
    internal class Feature2 : UpdateFeature
    {
        private readonly int _eventsPerFrame;

        public Feature2(int eventsPerFrame)
        {
            _eventsPerFrame = eventsPerFrame;
        }

        protected override void Initialize()
        {
            AddSystem(new System2(_eventsPerFrame, false));
            RegisterEvent<EcsEvent2>();
            RegisterRequest<EcsRequest2>();
        }
    }
}
