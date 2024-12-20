using Scellecs.Morpeh.Addons.Feature;

namespace Assets.Scripts.Benchmark.Features
{
    internal class Feature18 : UpdateFeature
    {
        private readonly int _eventsPerFrame;

        public Feature18(int eventsPerFrame)
        {
            _eventsPerFrame = eventsPerFrame;
        }

        protected override void Initialize()
        {
            AddSystem(new System18(_eventsPerFrame, false));
            RegisterEvent<EcsEvent18>();
            RegisterRequest<EcsRequest18>();
        }
    }
}
