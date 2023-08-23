using Scellecs.Morpeh.Addons.Feature;

namespace Assets.Scripts.Benchmark.Features
{
    internal class Feature13 : UpdateFeature
    {
        private readonly int _eventsPerFrame;

        public Feature13(int eventsPerFrame)
        {
            _eventsPerFrame = eventsPerFrame;
        }

        protected override void Initialize()
        {
            AddSystem(new System13(_eventsPerFrame, false));
            RegisterEvent<EcsEvent13>();
            RegisterRequest<EcsRequest13>();
        }
    }
}
