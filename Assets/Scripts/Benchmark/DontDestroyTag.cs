using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Assets.Scripts.Benchmark
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct DontDestroyTag : IComponent { }
}
