#if MORPEH_ADDONS_VCONTAINER

using System.Runtime.CompilerServices;
using Scellecs.Morpeh.Addons.Feature;
using VContainer;

namespace Scellecs.Morpeh.Addons.Unity.VContainer
{
    public static class ObjectResolverExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TFeature CreateFeature<TFeature>(this IObjectResolver container) 
            where TFeature : BaseFeature
        {
            var builder = new RegistrationBuilder(typeof(TFeature), Lifetime.Transient);
            var registration = builder.Build();
            return registration.SpawnInstance(container) as TFeature;
        }
    }
}

#endif