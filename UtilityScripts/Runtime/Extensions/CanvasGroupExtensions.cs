using System.Threading.Tasks;
using UnityEngine;

namespace HunterAllen.Utility
{
    public static class CanvasGroupExtensions
    {
        public static async Task LerpAlphaAsync(this CanvasGroup group, float targetAlpha, float _time)
        {
            float e = 0f;
            float a = group.alpha;

            while (e < _time)
            {
                if (Application.exitCancellationToken.IsCancellationRequested) return;
                
                e += Time.deltaTime;

                group.alpha = Mathf.Lerp(a, targetAlpha, e / _time);
                
                await Task.Yield();
            }
        }
        public static async void LerpAlpha(this CanvasGroup group, float targetAlpha, float _time)
        {
            float e = 0f;
            float a = group.alpha;

            while (e < _time)
            {
                if (Application.exitCancellationToken.IsCancellationRequested) return;
                
                e += Time.deltaTime;

                group.alpha = Mathf.Lerp(a, targetAlpha, e / _time);
                
                await Task.Yield();
            }
        }
    }
}