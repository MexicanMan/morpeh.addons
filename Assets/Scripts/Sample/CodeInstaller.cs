using Scellecs.Morpeh;

namespace Assets.Scripts.Sample
{
    public class CodeInstaller : BaseInstaller
    {
        private World _defaultWorld;
        private SystemsGroup[] _systemsGroups;

        protected void Awake()
        {
            InitializeShared();

            _systemsGroups = FillSystemsGroups();
        }

        protected override void OnEnable() => EnableGroups();

        protected override void OnDisable() => DisableGroups();

        protected void OnDestroy()
        {
            _defaultWorld.Dispose();
        }

        private SystemsGroup[] FillSystemsGroups()
        {
            return new[]
            {
                // Add your system groups
                _defaultWorld.CreateSystemsGroup()
            };
        }

        private void InitializeShared()
        {
            _defaultWorld = World.Default;

            // Add here some services init
        }

        private void EnableGroups()
        {
            for (int i = 0; i < _systemsGroups.Length; i++)
                _defaultWorld.AddSystemsGroup(i, _systemsGroups[i]);
        }

        private void DisableGroups()
        {
            for (int i = 0; i < _systemsGroups.Length; i++)
                _defaultWorld.RemoveSystemsGroup(_systemsGroups[i]);
        }

    }
}
