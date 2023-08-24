using Unity.IL2CPP.CompilerServices;

namespace Scellecs.Morpeh.Addons.EntityPool.Tags
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    internal struct PooledEntityTag : IComponent
    {
    }
}
