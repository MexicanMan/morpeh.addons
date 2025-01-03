﻿using JetBrains.Annotations;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Scellecs.Morpeh.Addons.Feature
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public static class FeatureWorldExtensions
    {
        [PublicAPI]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static World AddFeature(this World world, int order, BaseFeature feature, bool enabled = true)
        {
            feature.Register(world, order, enabled);
            FeatureRegistry.RegisterFeature(world, feature);

            return world;
        }

        [PublicAPI]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static World RemoveFeature(this World world, BaseFeature feature)
        {
            FeatureRegistry.RemoveFeature(world, feature);
            feature.Dispose();

            return world;
        }

        [PublicAPI]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetFeature<TFeature>(this World world, out TFeature feature)
                where TFeature : BaseFeature
        {
            feature = world.GetFeature<TFeature>();
        }

        [PublicAPI]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TFeature GetFeature<TFeature>(this World world)
                where TFeature : BaseFeature
        {
            return FeatureRegistry.GetFeature<TFeature>(world);
        }
    }
}
