using UnityEngine;

namespace Scellecs.Morpeh.Addons.Feature.Unity
{
    public abstract class BaseFeaturesInstaller : BaseInstaller
    {
        protected World defaultWorld;

        private UpdateFeature[] _updateFeatures;
        private FixedUpdateFeature[] _fixedUpdateFeatures;
        private LateUpdateFeature[] _lateUpdateFeatures;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void AddPlugins()
        {
            MorpehAddons.Initialize();
        }

        protected void Awake()
        {
            if (World.Default == null)
            {
                var world = World.Create("Default World");
                world.UpdateByUnity = true;
            }

            defaultWorld = World.Default;
            
            InitializeShared();
            InitializeFeatures();
        }

        protected sealed override void OnEnable() => EnableFeatures();

        protected sealed override void OnDisable() => DisableFeatures();

        protected void OnDestroy()
        {
            defaultWorld.Dispose();
        }

        // Here some services init
        protected abstract void InitializeShared();

        // Here some features init
        protected abstract UpdateFeature[] InitializeUpdateFeatures();

        protected abstract FixedUpdateFeature[] InitializeFixedUpdateFeatures();

        protected abstract LateUpdateFeature[] InitializeLateUpdateFeatures();

        private void InitializeFeatures()
        {
            _updateFeatures = InitializeUpdateFeatures();
            _fixedUpdateFeatures = InitializeFixedUpdateFeatures();
            _lateUpdateFeatures = InitializeLateUpdateFeatures();
        }

        private void EnableFeatures()
        {
            int order = 0;

            for (int i = 0; i < _updateFeatures.Length; i++, order++)
                defaultWorld.AddFeature(order, _updateFeatures[i]);
            for (int i = 0; i < _fixedUpdateFeatures.Length; i++, order++)
                defaultWorld.AddFeature(order, _fixedUpdateFeatures[i]);
            for (int i = 0; i < _lateUpdateFeatures.Length; i++, order++)
                defaultWorld.AddFeature(order, _lateUpdateFeatures[i]);
        }

        private void DisableFeatures()
        {
            for (int i = 0; i < _updateFeatures.Length; i++)
                defaultWorld.RemoveFeature(_updateFeatures[i]);
            for (int i = 0; i < _fixedUpdateFeatures.Length; i++)
                defaultWorld.RemoveFeature(_fixedUpdateFeatures[i]);
            for (int i = 0; i < _lateUpdateFeatures.Length; i++)
                defaultWorld.RemoveFeature(_lateUpdateFeatures[i]);
        }
    }
}
