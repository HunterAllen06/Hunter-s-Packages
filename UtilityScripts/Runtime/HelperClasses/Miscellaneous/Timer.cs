using System;
using UnityEngine;

namespace HunterAllen.Utility
{
    /// <summary>
    /// A simple timer that has events for starting, pausing and stopping.
    /// <para>The <i>Tick(float time)</i> function needs to be called for the timer to run.</para>
    /// </summary>
    public class Timer
    {
        float _time;
        public float Time { get => _time; }

        public bool IsRunning;

        public event Action OnTimerStart;
        public event Action OnTimerStop;
        public event Action OnTimerPause;
        public event Action OnTimerResume;

        float _elapsedTime;

        public Timer(float time)
        {
            _time = time;
        }

        public void SetTime(float time) => _time = time;

        public void Start()
        {
            IsRunning = true;
            _elapsedTime = _time;
            OnTimerStart?.Invoke();
        }

        public void Stop()
        {
            IsRunning = false;
            _elapsedTime = 0;
            OnTimerStop?.Invoke();
        }

        public void Pause()
        {
            IsRunning = false;
            OnTimerPause?.Invoke();
        }
        public void Resume()
        {
            IsRunning = true;
            OnTimerResume?.Invoke();
        }

        public void Tick(float time)
        {
            _elapsedTime -= time;
            
            if (_elapsedTime <= 0)
            {
                Stop();
            }
        }
    }
}