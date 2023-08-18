using Assets.Scripts.Sample.PlayerInput.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Feature.Events;
using Scellecs.Morpeh.Addons.Systems;
using UnityEngine;

namespace Assets.Scripts.Sample.PlayerInput.Systems
{
    internal sealed class KeyboardInputSystem : UpdateSystem
    {
        public override void OnAwake()
        {
        }

        public override void OnUpdate(float deltaTime)
        {
            Vector2 keyboardInput = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            ref var cInputEvent = ref World.CreateEventEntity<InputEvent>();
            cInputEvent.Input = keyboardInput;
            Debug.Log(cInputEvent.Input);
        }
    }
}
