using Unity.IL2CPP.CompilerServices;

namespace Scellecs.Morpeh.Addons.Systems
{

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public abstract class Initializer : IInitializer 
    {
        public World World { get; set; }

        public abstract void OnAwake();

        public virtual void Dispose() { }
    }
}