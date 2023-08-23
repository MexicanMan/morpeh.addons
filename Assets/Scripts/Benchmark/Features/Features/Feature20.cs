using Scellecs.Morpeh.Addons.Feature;

namespace Assets.Scripts.Benchmark.Features
{
    internal class Feature20 : UpdateFeature
    {
        private readonly int _eventsPerFrame;

        public Feature20(int eventsPerFrame)
        {
            _eventsPerFrame = eventsPerFrame;
        }

        protected override void Initialize()
        {
            AddSystem(new System20(_eventsPerFrame, false));
            RegisterEvent<EcsEvent20>();
            RegisterRequest<EcsRequest20>();
        }
    }
}
