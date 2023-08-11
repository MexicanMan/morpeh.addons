using Scellecs.Morpeh;
using Scellecs.Morpeh.Collections;
using System;

namespace Assets.Scripts.morpeh.feature
{
    internal sealed class FeatureRegistry : IDisposable
    {
        private static readonly IntHashMap<FeatureRegistry> WorldFeatureRegistry = new IntHashMap<FeatureRegistry>();

        private readonly World _world;

        public static FeatureRegistry GetFor(World world)
        {
            if (WorldFeatureRegistry.TryGetValue(world.identifier, out FeatureRegistry registry))
                return registry;

            registry = new FeatureRegistry(world);
            WorldFeatureRegistry.Add(world.identifier, registry, out _);
            return registry;
        }

        private FeatureRegistry(World world)
        {
            _world = world;
        }

        private void RegisterFeature<TFeature>(TFeature feature) where TFeature : BaseFeature<ISystem> 
        {
            for (var i = 0; i < registeredFilters; i++)
            {
                if (oneFrameFilters[i].GetInnerType() == typeof(T))
                {
                    return;
                }
            }

            if (registeredFilters >= oneFrameFilters.Length)
            {
                Array.Resize(ref oneFrameFilters, oneFrameFilters.Length << 1);
            }

            oneFrameFilters[registeredFilters++] = new OneFrameFilter<T>(world);
        }

        public void Dispose()
        {
            
        }
    }
}
