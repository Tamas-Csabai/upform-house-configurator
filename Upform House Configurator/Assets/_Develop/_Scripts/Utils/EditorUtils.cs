using UnityEngine;

namespace Upform
{
    public static class EditorUtils
    {

#if UNITY_EDITOR
        public static void EDITOR_GetComponents<T>(MonoBehaviour monoBehaviour, ref T[] objects) where T : UnityEngine.Object
        {
            UnityEditor.EditorUtility.SetDirty(monoBehaviour);

            objects = monoBehaviour.GetComponents<T>();

            UnityEditor.EditorUtility.SetDirty(monoBehaviour);
        }
#endif

    }
}
