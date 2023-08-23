using Scellecs.Morpeh.Addons.Feature;

namespace Assets.Scripts.Benchmark.Features
{
    internal class Feature15 : UpdateFeature
    {
        private readonly int _eventsPerFrame;

        public Feature15(int eventsPerFrame)
        {
            _eventsPerFrame = eventsPerFrame;
        }

        protected override void Initialize()
        {
            AddSystem(new System15(_eventsPerFrame, false));
            RegisterEvent<EcsEvent15>();
            RegisterRequest<EcsRequest15>();
        }
    }
}
