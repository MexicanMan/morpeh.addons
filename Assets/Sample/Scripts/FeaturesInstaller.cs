using Assets.Scripts.Sample.Logger;
using Assets.Scripts.Sample.PlayerInput;
using Assets.Scripts.Sample.Spawn;
using Scellecs.Morpeh.Addons.Feature;
using Scellecs.Morpeh.Addons.Feature.Unity;
using System;
using UnityEngine;

namespace Assets.Scripts.Sample
{
    public class FeaturesInstaller : BaseFeaturesInstaller
    {
        [SerializeField]
        private GameObject _cubePrefab;

        protected override void InitializeShared()
        {
        }

        protected override UpdateFeature[] InitializeUpdateFeatures()
        {
            return new UpdateFeature[]
            {
                new PlayerInputFeature(),
                new SpawnFeature(_cubePrefab),
                new LoggerFeature()
            };
        }

        protected override FixedUpdateFeature[] InitializeFixedUpdateFeatures()
        {
            return Array.Empty<FixedUpdateFeature>();
        }

        protected override LateUpdateFeature[] InitializeLateUpdateFeatures()
        {
            return Array.Empty<LateUpdateFeature>();
        }
    }
}
