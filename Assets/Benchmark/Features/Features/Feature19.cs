using Scellecs.Morpeh.Addons.Feature;

namespace Assets.Scripts.Benchmark.Features
{
    internal class Feature19 : UpdateFeature
    {
        private readonly int _eventsPerFrame;

        public Feature19(int eventsPerFrame)
        {
            _eventsPerFrame = eventsPerFrame;
        }

        protected override void Initialize()
        {
            AddSystem(new System19(_eventsPerFrame, false));
            RegisterEvent<EcsEvent19>();
            RegisterRequest<EcsRequest19>();
        }
    }
}
