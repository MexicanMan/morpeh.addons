using Scellecs.Morpeh.Addons.Feature;

namespace Assets.Scripts.Benchmark.Features
{
    internal class Feature14 : UpdateFeature
    {
        private readonly int _eventsPerFrame;

        public Feature14(int eventsPerFrame)
        {
            _eventsPerFrame = eventsPerFrame;
        }

        protected override void Initialize()
        {
            AddSystem(new System14(_eventsPerFrame, false));
            RegisterEvent<EcsEvent14>();
            RegisterRequest<EcsRequest14>();
        }
    }
}
