using UnityEngine;

namespace HunterAllen.Utility
{
    public static class HARandom
    {
        public static int CurrentSeed;

        /// <summary>
        /// Returns a random value between 0 and 1.
        /// </summary>
        /// <param name="seedOffset"></param>
        /// <returns></returns>
        public static float Value(int seedOffset)
        {
            Random.InitState(CurrentSeed + seedOffset);
            return Mathf.Clamp(Random.value, 0, 1);
        }

        /// <summary>
        /// Returns a random value between 0 and 1 given a Vector3 as a random offset.
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static float ValueFromWorldPosition(Vector3 pos)
        {
            float noiseValue = Mathf.PerlinNoise(pos.x, pos.y);
            float noiseValue2 = Mathf.PerlinNoise(noiseValue, pos.z);

            return Mathf.Clamp(Value(Mathf.RoundToInt(noiseValue2 * noiseValue * 185.7921f)), 0, 1);
        }
}
}