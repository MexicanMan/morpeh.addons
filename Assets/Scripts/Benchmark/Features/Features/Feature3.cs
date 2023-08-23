using Scellecs.Morpeh.Addons.Feature;

namespace Assets.Scripts.Benchmark.Features
{
    internal class Feature3 : UpdateFeature
    {
        private readonly int _eventsPerFrame;

        public Feature3(int eventsPerFrame)
        {
            _eventsPerFrame = eventsPerFrame;
        }

        protected override void Initialize()
        {
            AddSystem(new System3(_eventsPerFrame, false));
            RegisterEvent<EcsEvent3>();
            RegisterRequest<EcsRequest3>();
        }
    }
}
