using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Assets.Scripts.Sample.Spawn.Components.Events
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct SpawnedEvent : IComponent
    {
    }
}
