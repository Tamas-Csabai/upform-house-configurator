using UnityEngine;

namespace Upform
{
    public static class Extender
    {

        public static float Remap(this float f, float fromMin, float fromMax, float toMin, float toMax)
        {
            return ((Mathf.Clamp(f, fromMin, fromMax) - fromMin) / (fromMax - fromMin) * (toMax - toMin)) + toMin;
        }

    }
}
