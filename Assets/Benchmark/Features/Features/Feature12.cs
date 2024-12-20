using Scellecs.Morpeh.Addons.Feature;

namespace Assets.Scripts.Benchmark.Features
{
    internal class Feature12 : UpdateFeature
    {
        private readonly int _eventsPerFrame;

        public Feature12(int eventsPerFrame)
        {
            _eventsPerFrame = eventsPerFrame;
        }

        protected override void Initialize()
        {
            AddSystem(new System12(_eventsPerFrame, false));
            RegisterEvent<EcsEvent12>();
            RegisterRequest<EcsRequest12>();
        }
    }
}
