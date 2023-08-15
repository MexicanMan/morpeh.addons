using JetBrains.Annotations;
using System;

namespace Scellecs.Morpeh.Addons.Feature
{
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
    }
}