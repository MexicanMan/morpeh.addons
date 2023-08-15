using Scellecs.Morpeh.Collections;
using System;
using System.Runtime.CompilerServices;

namespace Scellecs.Morpeh.Addons.Feature
{
    internal sealed class FeatureRegistry : IDisposable
    {
        private static readonly IntHashMap<FeatureRegistry> WorldFeatureRegistry = new IntHashMap<FeatureRegistry>();

        private readonly World _world;
        private FastList<BaseFeature> _registeredFeatures;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InitializeFeatureRegistry(World world)
        {
            var registry = new FeatureRegistry(world);
            WorldFeatureRegistry.Add(world.identifier, registry, out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FeatureRegistry GetFor(World world)
        {
            return WorldFeatureRegistry.GetValueByKey(world.identifier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RegisterFeature(World world, BaseFeature feature)
        {
            GetFor(world).RegisterFeature(feature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveFeature(World world, BaseFeature feature)
        {
            GetFor(world).RemoveFeature(feature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TFeature GetFeature<TFeature>(World world)
            where TFeature : BaseFeature
        {
            return GetFor(world).GetFeature<TFeature>();
        }

        private FeatureRegistry(World world)
        {
            _world = world;

            _registeredFeatures = new FastList<BaseFeature>();
        }

        private void RegisterFeature(BaseFeature feature)
        {
            foreach (var registeredFeature in _registeredFeatures)
            {
                if (registeredFeature.GetType() == feature.GetType())
                {
                #if MORPEH_DEBUG
                    MLogger.LogError($"Only one instance of the \"{feature.GetType()}\" feature could be registered!");
                #endif

                    return;
                }
            }

            _registeredFeatures.Add(feature);
        }

        private void RemoveFeature(BaseFeature feature)
        {
            int featureIndex = _registeredFeatures.IndexOf(feature);
            if (featureIndex == -1)
            {
            #if MORPEH_DEBUG
                MLogger.LogWarning($"There wasn't registered any features with the type of \"{feature.GetType()}\"!");
            #endif

                return;
            }

            _registeredFeatures.RemoveAt(featureIndex);
        }

        private TFeature GetFeature<TFeature>() where TFeature : BaseFeature
        {
            foreach (var registeredFeature in _registeredFeatures)
                if (registeredFeature.GetType() == typeof(TFeature))
                    return (TFeature) registeredFeature;

        #if MORPEH_DEBUG
            MLogger.LogWarning($"There wasn't registered any features with the type of \"{typeof(TFeature)}\"!");
        #endif

            return null;
        }

        private void ClearRegisteredFeatures()
        {
            foreach (var registeredFeature in _registeredFeatures)
                registeredFeature.Dispose();

            _registeredFeatures.Clear();
        }

        public void Dispose()
        {
            ClearRegisteredFeatures();
            WorldFeatureRegistry.Remove(_world.identifier, out _);
        }
    }
}
