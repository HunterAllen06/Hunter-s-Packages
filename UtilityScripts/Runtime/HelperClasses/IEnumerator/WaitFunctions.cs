using System.Collections.Generic;
using UnityEngine;

namespace HunterAllen.Utility
{
    public static class WaitFunctions
    {
        static Dictionary<float, WaitForSeconds> _waitForSeconds = new Dictionary<float, WaitForSeconds>();
        public static WaitForEndOfFrame WaitForFrame = new WaitForEndOfFrame();

        public static WaitForSeconds WaitForSeconds(float time)
        {
            if (_waitForSeconds.TryGetValue(time, out WaitForSeconds wait))
            {
                return wait;
            }

            _waitForSeconds.Add(time, new WaitForSeconds(time));

            return _waitForSeconds[time];
        }
    }
}