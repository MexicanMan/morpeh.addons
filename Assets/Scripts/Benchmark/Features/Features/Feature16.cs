using Scellecs.Morpeh.Addons.Feature;

namespace Assets.Scripts.Benchmark.Features
{
    internal class Feature16 : UpdateFeature
    {
        private readonly int _eventsPerFrame;

        public Feature16(int eventsPerFrame)
        {
            _eventsPerFrame = eventsPerFrame;
        }

        protected override void Initialize()
        {
            AddSystem(new System16(_eventsPerFrame, false));
            RegisterEvent<EcsEvent16>();
            RegisterRequest<EcsRequest16>();
        }
    }
}
