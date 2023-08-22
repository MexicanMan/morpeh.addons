using Scellecs.Morpeh;
using UnityEngine;

namespace Assets.Scripts.Benchmark.Features
{
    public class DefaultBenchmarkInstaller : BaseInstaller
    {
        [SerializeField]
        private int _eventsPerFrame = 1000;
        
        private SystemsGroup _systemsGroup;
        private World _defaultWorld;

        protected void Awake()
        {
            _defaultWorld = World.Default;
            _systemsGroup = CreateCommonGroup();
        }
        
        protected override void OnEnable() => EnableGroups();
        protected override void OnDisable() => DisableGroups();

        private SystemsGroup CreateCommonGroup()
        {
            var eventsPerSystem = _eventsPerFrame / FeaturesBenchmarkInstaller.FeaturesCount;
            var common = _defaultWorld.CreateSystemsGroup();
            
            common.AddInitializer(new RegisterOneFrameSystem());
            common.AddSystem(new System1(eventsPerSystem, false));
            common.AddSystem(new System2(eventsPerSystem, false));
            common.AddSystem(new System3(eventsPerSystem, false));
            common.AddSystem(new System4(eventsPerSystem, false));
            common.AddSystem(new System5(eventsPerSystem, false));
            
            return common;
        }

        private void EnableGroups()
        {
            _defaultWorld.AddSystemsGroup(0, _systemsGroup);
        }

        private void DisableGroups()
        {
            _defaultWorld.RemoveSystemsGroup(_systemsGroup);
        }
    }
}
