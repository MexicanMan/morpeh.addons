namespace Scellecs.Morpeh.Helpers.OneFrame {
    using UnityEngine.Scripting;

    [Preserve]
    internal sealed class OneFramePlugin : IWorldPlugin
    {
        private SystemsGroup systemsGroup;
        
        [Preserve]
        public OneFramePlugin() { }

        [Preserve]
        public void Initialize(World world) {
            systemsGroup = world.CreateSystemsGroup();
            systemsGroup.AddSystem(new OneFrameCleanSystem());
            world.AddPluginSystemsGroup(systemsGroup);
        }

        public void Deinitialize(World world)
        {
            world.RemoveSystemsGroup(systemsGroup);
        }
    }
}