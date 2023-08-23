using Scellecs.Morpeh.Addons.Feature;

namespace Assets.Scripts.Benchmark.Features
{
    internal class Feature11 : UpdateFeature
    {
        private readonly int _eventsPerFrame;

        public Feature11(int eventsPerFrame)
        {
            _eventsPerFrame = eventsPerFrame;
        }

        protected override void Initialize()
        {
            AddSystem(new System11(_eventsPerFrame, false));
            RegisterEvent<EcsEvent11>();
            RegisterRequest<EcsRequest11>();
        }
    }
}
