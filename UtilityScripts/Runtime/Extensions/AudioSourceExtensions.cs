using System.Threading.Tasks;
using UnityEngine;

namespace HunterAllen.Utility
{
    public static class AudioSourceExtensions
    {
        public static async Task LerpVolumeAsync(this AudioSource source, float targetVolume, float _time)
        {
            float e = 0f;
            float a = source.volume;

            while (e < _time)
            {
                if (Application.exitCancellationToken.IsCancellationRequested) return;
                
                e += Time.deltaTime;

                source.volume = Mathf.Lerp(a, targetVolume, e / _time);
                
                await Task.Yield();
            }
        }
        public static async void LerpVolume(this AudioSource source, float targetVolume, float _time)
        {
            float e = 0f;
            float a = source.volume;

            while (e < _time)
            {
                if (Application.exitCancellationToken.IsCancellationRequested) return;
                
                e += Time.deltaTime;

                source.volume = Mathf.Lerp(a, targetVolume, e / _time);
                
                await Task.Yield();
            }
        }
    }
}