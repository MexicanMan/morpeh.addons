using Scellecs.Morpeh.Addons.Feature;

namespace Assets.Scripts.Benchmark.Features
{
    internal class Feature8 : UpdateFeature
    {
        private readonly int _eventsPerFrame;

        public Feature8(int eventsPerFrame)
        {
            _eventsPerFrame = eventsPerFrame;
        }

        protected override void Initialize()
        {
            AddSystem(new System8(_eventsPerFrame, false));
            RegisterEvent<EcsEvent8>();
            RegisterRequest<EcsRequest8>();
        }
    }
}
