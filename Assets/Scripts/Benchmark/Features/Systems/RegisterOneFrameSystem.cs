using Scellecs.Morpeh.Addons.Systems;
using Scellecs.Morpeh.Helpers.OneFrame;

namespace Assets.Scripts.Benchmark.Features
{
    internal sealed class RegisterOneFrameSystem : Initializer
    {
        public override void OnAwake()
        {
            World.RegisterOneFrame<EcsEvent1>();
            World.RegisterOneFrame<EcsEvent2>();
            World.RegisterOneFrame<EcsEvent3>();
            World.RegisterOneFrame<EcsEvent4>();
            World.RegisterOneFrame<EcsEvent5>();
            
            World.RegisterOneFrame<EcsRequest1>();
            World.RegisterOneFrame<EcsRequest2>();
            World.RegisterOneFrame<EcsRequest3>();
            World.RegisterOneFrame<EcsRequest4>();
            World.RegisterOneFrame<EcsRequest5>();
        }
    }
}
