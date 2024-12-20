using Scellecs.Morpeh.Addons.Feature;

namespace Assets.Scripts.Benchmark.Features
{
    internal class Feature7 : UpdateFeature
    {
        private readonly int _eventsPerFrame;

        public Feature7(int eventsPerFrame)
        {
            _eventsPerFrame = eventsPerFrame;
        }

        protected override void Initialize()
        {
            AddSystem(new System7(_eventsPerFrame, false));
            RegisterEvent<EcsEvent7>();
            RegisterRequest<EcsRequest7>();
        }
    }
}
