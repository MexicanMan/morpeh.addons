using Scellecs.Morpeh.Addons.Feature;

namespace Assets.Scripts.Benchmark.Features
{
    internal class Feature4 : UpdateFeature
    {
        private readonly int _eventsPerFrame;

        public Feature4(int eventsPerFrame)
        {
            _eventsPerFrame = eventsPerFrame;
        }

        protected override void Initialize()
        {
            AddSystem(new System4(_eventsPerFrame, false));
            RegisterEvent<EcsEvent4>();
            RegisterRequest<EcsRequest4>();
        }
    }
}
