using JetBrains.Annotations;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Scellecs.Morpeh.Addons.OneShot
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
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
