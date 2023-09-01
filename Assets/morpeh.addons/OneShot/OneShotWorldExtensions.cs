using JetBrains.Annotations;
using System.Runtime.CompilerServices;

namespace Scellecs.Morpeh.Addons.OneShot
{
    public static class OneShotWorldExtensions
    {
        [PublicAPI]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static World RegisterOneShot<TOneShot>(this World world) where TOneShot : struct, IComponent
        {
            OneShotRegistry.RegisterOneShot<TOneShot>(world);

            return world;
        }
    }
}
