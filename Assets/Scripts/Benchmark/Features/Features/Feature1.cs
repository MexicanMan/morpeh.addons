using Scellecs.Morpeh.Addons.Feature;

namespace Assets.Scripts.Benchmark.Features
{
    internal class Feature1 : UpdateFeature
    {
        private readonly int _eventsPerFrame;

        public Feature1(int eventsPerFrame)
        {
            _eventsPerFrame = eventsPerFrame;
        }

        protected override void Initialize()
        {
            AddSystem(new System1(_eventsPerFrame, true));
            RegisterEvent<EcsEvent1>();
        }
    }
}
