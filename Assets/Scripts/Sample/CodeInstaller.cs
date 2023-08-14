using Scellecs.Morpeh;
using Assets.Scripts.Sample.PlayerInput;
using Scellecs.Morpeh.Addons.Feature;
using UnityEngine;

namespace Assets.Scripts.Sample
{
    public class CodeInstaller : BaseInstaller
    {
        private World _defaultWorld;
        private UpdateFeature[] _updateFeatures;
        private FixedUpdateFeature[] _fixedUpdateFeatures;
        private LateUpdateFeature[] _lateUpdateFeatures;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void AddPlugins()
        {
            WorldExtensions.AddWorldPlugin(new FeaturePlugin());
        }

        protected void Awake()
        {
            InitializeShared();

            InitializeFeatures();
        }

        protected override void OnEnable() => EnableFeatures();

        protected override void OnDisable() => DisableFeatures();

        protected void OnDestroy()
        {
            _defaultWorld.Dispose();
        }

        private void InitializeShared()
        {
            _defaultWorld = World.Default;

            // Add here some services init
        }

        private void InitializeFeatures()
        {
            _updateFeatures = new UpdateFeature[]
            {
                new PlayerInputFeature(),
            };

            _fixedUpdateFeatures = new FixedUpdateFeature[] { };

            _lateUpdateFeatures = new LateUpdateFeature[] { };
        }

        private void EnableFeatures()
        {
            int order = 0;

            for (int i = 0; i < _updateFeatures.Length; i++, order++)
                _defaultWorld.AddFeature(order, _updateFeatures[i]);
            for (int i = 0; i < _fixedUpdateFeatures.Length; i++, order++)
                _defaultWorld.AddFeature(order, _fixedUpdateFeatures[i]);
            for (int i = 0; i < _lateUpdateFeatures.Length; i++, order++)
                _defaultWorld.AddFeature(order, _lateUpdateFeatures[i]);
        }

        private void DisableFeatures()
        {
            for (int i = 0; i < _updateFeatures.Length; i++)
                _defaultWorld.RemoveFeature(_updateFeatures[i]);
            for (int i = 0; i < _fixedUpdateFeatures.Length; i++)
                _defaultWorld.RemoveFeature(_fixedUpdateFeatures[i]);
            for (int i = 0; i < _lateUpdateFeatures.Length; i++)
                _defaultWorld.RemoveFeature(_lateUpdateFeatures[i]);
        }
    }
}
