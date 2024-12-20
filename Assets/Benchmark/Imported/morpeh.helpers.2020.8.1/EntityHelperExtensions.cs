namespace Scellecs.Morpeh.Helpers {
    using System;
    using System.Runtime.CompilerServices;
    using JetBrains.Annotations;

    public static class EntityHelperExtensions {
        [PublicAPI]
        [Obsolete("Use " + nameof(EntityExtensions.IsNullOrDisposed))]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Exists(this Entity entity) {
            return entity != null && !entity.IsDisposed();
        }

        [PublicAPI]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T GetOrCreate<T>(this Entity entity)
                where T : struct, IComponent {
            if (entity.Has<T>()) {
                return ref entity.GetComponent<T>();
            }

            return ref entity.AddComponent<T>();
        }
    }
}