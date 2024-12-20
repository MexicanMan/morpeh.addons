using Scellecs.Morpeh.Addons.Feature;
using Scellecs.Morpeh.Addons.Feature.Unity;
using UnityEngine;

namespace Assets.Scripts.Benchmark.Features
{
    public class FeaturesBenchmarkInstaller : BaseFeaturesInstaller
    {
        public const int FeaturesCount = 20;
        
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
                new Feature6(_eventsPerFrame / FeaturesCount),
                new Feature7(_eventsPerFrame / FeaturesCount),
                new Feature8(_eventsPerFrame / FeaturesCount),
                new Feature9(_eventsPerFrame / FeaturesCount),
                new Feature10(_eventsPerFrame / FeaturesCount),
                new Feature11(_eventsPerFrame / FeaturesCount),
                new Feature12(_eventsPerFrame / FeaturesCount),
                new Feature13(_eventsPerFrame / FeaturesCount),
                new Feature14(_eventsPerFrame / FeaturesCount),
                new Feature15(_eventsPerFrame / FeaturesCount),
                new Feature16(_eventsPerFrame / FeaturesCount),
                new Feature17(_eventsPerFrame / FeaturesCount),
                new Feature18(_eventsPerFrame / FeaturesCount),
                new Feature19(_eventsPerFrame / FeaturesCount),
                new Feature20(_eventsPerFrame / FeaturesCount),
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
