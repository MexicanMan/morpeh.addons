using System;
using Scellecs.Morpeh.Addons.Feature;
using Scellecs.Morpeh.Addons.Feature.Unity;
using UnityEngine;

namespace Assets.Scripts.Benchmark.EventEntityPool
{
    public class EventEntityPoolBenchmarkInstaller : BaseFeaturesInstaller
    {
        [SerializeField]
        private int _eventsPerFrame = 1000;
        
        [SerializeField]
        private BenchmarkType _benchmarkType;
        
        protected override void InitializeShared()
        {
        }

        protected override UpdateFeature[] InitializeUpdateFeatures()
        {
            var features = new UpdateFeature[1];

            features[0] = _benchmarkType switch
            {
                BenchmarkType.OldEntity => new OldEntityFeature(_eventsPerFrame),
                BenchmarkType.NewEntity => new NewEntityFeature(_eventsPerFrame),
                BenchmarkType.PoolV1 => new PoolV1Feature(_eventsPerFrame),
                _ => throw new ArgumentOutOfRangeException()
            };
            
            return features;
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

    public enum BenchmarkType
    {
        OldEntity,
        NewEntity,
        PoolV1,
    }
}
