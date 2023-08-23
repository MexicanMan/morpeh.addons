﻿using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Assets.Scripts.Benchmark.Features
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct EcsRequest13 : IComponent { }
}
