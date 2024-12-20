using Scellecs.Morpeh.Addons.Feature;

namespace Assets.Scripts.Benchmark.Features
{
    internal class Feature9 : UpdateFeature
    {
        private readonly int _eventsPerFrame;

        public Feature9(int eventsPerFrame)
        {
            _eventsPerFrame = eventsPerFrame;
        }

        protected override void Initialize()
        {
            AddSystem(new System9(_eventsPerFrame, false));
            RegisterEvent<EcsEvent9>();
            RegisterRequest<EcsRequest9>();
        }
    }
}
