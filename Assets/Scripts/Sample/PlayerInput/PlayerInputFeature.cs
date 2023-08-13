using Assets.Scripts.morpeh.feature;
using Assets.Scripts.Sample.PlayerInput.Systems;

namespace Assets.Scripts.Sample.PlayerInput
{
    internal class PlayerInputFeature : UpdateFeature
    {
        protected override void Initialize()
        {
            AddSystem(new KeyboardInputSystem());
        }
    }
}
