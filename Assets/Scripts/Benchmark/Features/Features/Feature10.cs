using Scellecs.Morpeh.Addons.Feature;

namespace Assets.Scripts.Benchmark.Features
{
    internal class Feature10 : UpdateFeature
    {
        private readonly int _eventsPerFrame;

        public Feature10(int eventsPerFrame)
        {
            _eventsPerFrame = eventsPerFrame;
        }

        protected override void Initialize()
        {
            AddSystem(new System10(_eventsPerFrame, false));
            RegisterEvent<EcsEvent10>();
            RegisterRequest<EcsRequest10>();
        }
    }
}
