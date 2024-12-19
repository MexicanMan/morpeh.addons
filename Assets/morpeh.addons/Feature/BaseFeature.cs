using JetBrains.Annotations;
using System;
using Scellecs.Morpeh.Collections;
using Unity.IL2CPP.CompilerServices;

namespace Scellecs.Morpeh.Addons.Feature
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public abstract class BaseFeature : IDisposable
    {
        private protected World world;
        private protected SystemsGroup featureSystemsGroup;

        private protected bool enabled = true;

        [PublicAPI]
        public void AddInitializer(IInitializer initializer)
        {
            featureSystemsGroup.AddInitializer(initializer);
        }

        [PublicAPI]
        public abstract void Enable();

        [PublicAPI]
        public abstract void Disable();

        internal abstract void Register(World world, int order, bool enabled);

        public abstract void Dispose();

        protected static void MoveSystemsList(FastList<ISystem> fromSystems, FastList<ISystem> toSystems)
        {
            foreach (var system in fromSystems)
                toSystems.Add(system);
            fromSystems.Clear();
        }
    }
}