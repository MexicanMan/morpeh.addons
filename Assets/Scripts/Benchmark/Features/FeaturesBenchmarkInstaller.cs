using Scellecs.Morpeh.Addons.Feature;
using Scellecs.Morpeh.Addons.Feature.Unity;
using UnityEngine;

namespace Assets.Scripts.Benchmark.Features
{
    public class FeaturesBenchmarkInstaller : BaseFeaturesInstaller
    {
        public const int FeaturesCount = 5;
        
        [SerializeField]
        private int _eventsPerFrame = 1000;

        protected override void InitializeShared()
        {
        }

        protected override UpdateFeature[] InitializeUpdateFeatures()
        {
            return new UpdateFeature[]
            {
                new Feature1(_eventsPerFrame / FeaturesCount),
                new Feature2(_eventsPerFrame / FeaturesCount),
                new Feature3(_eventsPerFrame / FeaturesCount),
                new Feature4(_eventsPerFrame / FeaturesCount),
                new Feature5(_eventsPerFrame / FeaturesCount),
            };
        }

        protected override FixedUpdateFeature[] InitializeFixedUpdateFeatures()
        {
            return new FixedUpdateFeature[] { };
        }

        protected override LateUpdateFeature[] InitializeLateUpdateFeatures()
        {
            return new LateUpdateFeature[] { };
        }
    }
}
