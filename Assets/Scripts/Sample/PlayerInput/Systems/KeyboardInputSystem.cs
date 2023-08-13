using Assets.Scripts.Sample.PlayerInput.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Assets.Scripts.Sample.PlayerInput.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    internal sealed class KeyboardInputSystem : ISystem
    {
        public World World { get; set; }

        public void OnAwake()
        {
        }

        public void OnUpdate(float deltaTime)
        {
            Vector2 keyboardInput = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            ref var cInputEvent = ref World.CreateEntity().AddComponent<InputEvent>();
            cInputEvent.Input = keyboardInput;
            Debug.Log(cInputEvent.Input);
        }

        public void Dispose() { }
    }
}
